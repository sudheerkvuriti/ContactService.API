using LeadService.Application.Interfaces;
using LeadService.Application.Interfaces.Repositories;
using LeadService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadService.API.Controllers
{
    [Authorize]  
    [Route("api/[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        // -------------------------------------------------------
        // POST: api/lead
        // Create Lead with Address & Communication Details
        // -------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> CreateLead([FromBody] LeadCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Map DTO → Lead Entity
                var lead = new Lead
                {
                    FirstName = dto.LeadName,
                    LeadSourceID = dto.LeadSourceID,
                    CreatedDate = DateTime.UtcNow
                };

                // Map DTO → Address
                var address = new LeadAddress
                {
                    AddressType = dto.Address.AddressType,
                    Address = new Address
                    {

                        CityID = dto.Address.CityID,
                        StateID = dto.Address.StateID,
                        CountryID = dto.Address.CountryID,
                        ZipCode = dto.Address.Pincode
                    }
                };

                // Map DTO → Communication Details
                LeadCommDetail objCommDetails = new LeadCommDetail();
                if (objCommDetails.CommTypeID == 1)
                {
                    objCommDetails.CommSubTypeName = dto.Email;
                }
                if (objCommDetails.CommTypeID == 2)
                {
                    objCommDetails.CommSubTypeName = dto.PhoneNumber;
                }
                if (objCommDetails.CommTypeID == 3)
                {
                    objCommDetails.CommSubTypeName = dto.AlternatePhone;
                }

                

                var createdLead = await _leadService.CreateLeadAsync(lead, address, objCommDetails);

                return CreatedAtAction(nameof(GetLeadById),
                    new { id = createdLead.LeadID },
                    createdLead);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLeadById(int id)
        {
            var lead = await _leadService.GetLeadAsync(id);

            if (lead == null)
                return NotFound(new { Message = "Lead not found" });

            return Ok(lead);
        }
    }
}
