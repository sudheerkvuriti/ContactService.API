using LeadService.Domain.Entities;

namespace LeadService.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(string email, string password);
    }
}
