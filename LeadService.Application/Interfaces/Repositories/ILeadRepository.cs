
using LeadService.Domain.Entities;
namespace LeadService.Application.Interfaces.Repositories
{
    public interface ILeadRepository
    {
        Task<Lead> AddLeadAsync(Lead lead);
        Task<Lead?> GetByIdAsync(int leadId);
    }
}
