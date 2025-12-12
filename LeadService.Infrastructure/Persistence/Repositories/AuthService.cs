using LeadService.Application.Interfaces.Repositories;
using LeadService.Application.Interfaces.Services;
using LeadService.Domain.Entities;
using System.Text;

namespace LeadService.Infrastructure.Persistence.Repositories
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
                return null;

            if (user.Password != HashPassword(password))
                return null;

            return user;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

    }
}
