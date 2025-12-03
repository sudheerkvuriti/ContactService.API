using ContactServices.API.DTOs;

namespace ContactServices.API.Repository.Interfaces
{
    public interface IContactService
    {
        Task<CreateContactDto> CreateContactAsync(CreateContactDto request);
        Task<CreateContactDto> UpdateContactAsync(int id, CreateContactDto request);
        Task<bool> DeleteContactAsync(int id);
        Task<ContactResponseDto> GetContactByIdAsync(int id);
        Task<List<ContactResponseDto>> GetAllContactsAsync();
    }
}
