using LeadService.Application.Interfaces.Repositories;
using LeadService.Domain.Entities;

namespace LeadService.Infrastructure.Persistence.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly LeadDbContext _context;

        public AddressRepository(LeadDbContext context)
        {
            _context = context;
        }

        public async Task<LeadAddress> AddLeadAddressAsync(LeadAddress leadAddress)
        {
            _context.LeadAddresses.Add(leadAddress);
            await _context.SaveChangesAsync();
            return leadAddress;
        }
    }
}
