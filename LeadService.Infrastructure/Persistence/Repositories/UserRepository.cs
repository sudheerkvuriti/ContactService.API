using LeadService.Application.Interfaces.Repositories;
using LeadService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadService.Infrastructure.Persistence.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly LeadDbContext _context;
        public UserRepository(LeadDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.EmailID == email);
        }
    }
}
