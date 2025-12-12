using LeadService.Domain.Entities;

namespace LeadService.Application.Interfaces.Repositories
{
    public interface ILeadCommDetailRepository
    {
        Task<LeadCommDetail> AddCommDetailAsync(LeadCommDetail commDetail);
    }
}
