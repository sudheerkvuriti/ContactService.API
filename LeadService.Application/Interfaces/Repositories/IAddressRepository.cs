using LeadService.Domain.Entities;

namespace LeadService.Application.Interfaces.Repositories
{
    public interface IAddressRepository
    {
        Task<LeadAddress> AddLeadAddressAsync(LeadAddress leadAddress);
    }
}
