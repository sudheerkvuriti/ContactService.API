using AutoMapper;
using ContactServices.API.DTOs;
using ContactServices.API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        // POST: api/contact
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactDto dto)
        {
            var result = await _contactService.CreateContactAsync(dto);
            return Ok(result);
        }

        // GET: api/contact/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var result = await _contactService.GetContactByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/contact
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetAllContactsAsync();
            return Ok(result);
        }

        // PUT: api/contact/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] CreateContactDto dto)
        {
            var updated = await _contactService.UpdateContactAsync(id, dto);
            return Ok("Contact updated successfully.");
        }

        // DELETE: api/contact/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var deleted = await _contactService.DeleteContactAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Contact deleted successfully.");
        }
    }
}
