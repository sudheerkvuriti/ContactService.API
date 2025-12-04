using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ContactService.API.Models;

namespace ContactService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwt;

        public AuthController(IOptions<JwtSettings> jwtOptions)
        {
            _jwt = jwtOptions.Value;
        }

        // Demo endpoint: POST api/auth/token
        // Accepts a simple payload { "username": "alice", "role": "Admin" } for dev testing
        [HttpPost("token")]
        public IActionResult GetToken([FromBody] TokenRequest req)
        {
            // In real life: validate credentials (DB, Identity, external)
            if (string.IsNullOrEmpty(req.Username))
                return BadRequest("username required");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, req.Username),
                new Claim("scope", "contacts.create contacts.read contacts.update"),
                new Claim(ClaimTypes.Name, req.Username)
            };

            if (!string.IsNullOrEmpty(req.Role))
                claims.Add(new Claim(ClaimTypes.Role, req.Role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwt.ExpiresMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = expires
            });
        }
    }

    public class TokenRequest
    {
        public string Username { get; set; } = "";
        public string? Role { get; set; } = null;
    }
}
