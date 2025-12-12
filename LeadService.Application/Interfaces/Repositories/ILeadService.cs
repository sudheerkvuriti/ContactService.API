using LeadService.Domain.Entities;

namespace LeadService.Application.Interfaces.Repositories
{
    public interface ILeadService
    {
        Task<Lead> CreateLeadAsync(Lead lead, LeadAddress address, LeadCommDetail commDetail);
        Task<Lead?> GetLeadAsync(int leadId);
    }
}
