using AutoMapper;
using ContactServices.API.DTOs;
using ContactServices.API.Models;
namespace ContactServices.API.Mappings
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            // Entity → DTO
            CreateMap<Contact, ContactDto>();

            // DTO → Entity
            CreateMap<CreateContactDto, Contact>();
        }
    }
}
