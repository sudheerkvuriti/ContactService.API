using Azure.Core;
using LeadService.API.Models;
using LeadService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private static List<UserStore> Users = new(); // Temporary In-Memory Database
    private readonly IAuthService _authService;
    private readonly JwtTokenService _tokenService;

    public AuthController(IAuthService authService, JwtTokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }


    // Register User
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        if (Users.Any(u => u.EmailID == request.EmailID))
            return BadRequest("User already exists.");

        Users.Add(new UserStore
        {
            EmailID = request.EmailID,
            PasswordHash = HashPassword(request.Password),
            Role = request.Role
        });

        return Ok("User Registered Successfully");
    }

    // Login User
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _authService.ValidateUserAsync(request.EmailID, request.Password);

        if (user == null)
            return Unauthorized("Invalid email or password");

        var token = _tokenService.GenerateToken(user.EmailID, user.RoleID.ToString());
        var refreshToken = _tokenService.GenerateRefreshToken();

        return Ok(new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            Expiration = DateTime.Now.AddMinutes(60)
        });
    }


    // Password hashing
    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }


}

public class UserStore
{
    public string EmailID { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
}
