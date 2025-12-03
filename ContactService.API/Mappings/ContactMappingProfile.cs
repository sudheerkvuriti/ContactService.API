using AutoMapper;
using ContactServices.API.DTOs;
using ContactServices.API.Models;

namespace ContactServices.API.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<ContactRequestDto, Contact>();
            CreateMap<Contact, ContactResponseDto>();
        }
    }
}
