using LeadService.Application.Interfaces.Repositories;
using LeadService.Domain.Entities;

namespace LeadService.Infrastructure.Services
{
    public class LeadServiceImplementation: ILeadService
    {
        private readonly ILeadRepository _leadRepo;
        private readonly IAddressRepository _addressRepo;
        private readonly ILeadCommDetailRepository _commRepo;

        public LeadServiceImplementation(
            ILeadRepository leadRepo,
            IAddressRepository addressRepo,
            ILeadCommDetailRepository commRepo)
        {
            _leadRepo = leadRepo;
            _addressRepo = addressRepo;
            _commRepo = commRepo;
        }

        public async Task<Lead> CreateLeadAsync(Lead lead, LeadAddress address, LeadCommDetail commDetail)
        {
            var createdLead = await _leadRepo.AddLeadAsync(lead);

            address.LeadID = createdLead.LeadID;
            await _addressRepo.AddLeadAddressAsync(address);

            commDetail.LeadID = createdLead.LeadID;
            await _commRepo.AddCommDetailAsync(commDetail);

            return createdLead;
        }

        public Task<Lead?> GetLeadAsync(int leadId)
        {
            return _leadRepo.GetByIdAsync(leadId);
        }
    }
}


