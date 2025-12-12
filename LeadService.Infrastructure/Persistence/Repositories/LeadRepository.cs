using LeadService.Application.Interfaces.Repositories;
using LeadService.Domain.Entities;
using LeadService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LeadService.Infrastructure.Persistence.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly LeadDbContext _context;

        public LeadRepository(LeadDbContext context)
        {
            _context = context;
        }

        public async Task<Lead> AddLeadAsync(Lead lead)
        {
            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();
            return lead;
        }

        public async Task<Lead?> GetByIdAsync(int leadId)
        {
            return await _context.Leads
                .Include(l => l.LeadAddresses)
                    .ThenInclude(la => la.Address)
                .FirstOrDefaultAsync(l => l.LeadID == leadId);
        }
    }
}
