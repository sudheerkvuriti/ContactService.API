using LeadService.Domain.Entities;

namespace LeadService.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
