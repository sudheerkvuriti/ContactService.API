using LeadService.Application.Interfaces.Repositories;
using LeadService.Domain.Entities;

namespace LeadService.Infrastructure.Persistence.Repositories
{
    public class LeadCommDetailRepository : ILeadCommDetailRepository
    {
        private readonly LeadDbContext _context;

        public LeadCommDetailRepository(LeadDbContext context)
        {
            _context = context;
        }

        public async Task<LeadCommDetail> AddCommDetailAsync(LeadCommDetail comm)
        {
            _context.LeadCommDetails.Add(comm);
            await _context.SaveChangesAsync();
            return comm;
        }
    }
}
