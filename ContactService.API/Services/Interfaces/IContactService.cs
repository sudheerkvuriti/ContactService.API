using ContactServices.API.DTOs;

namespace ContactServices.API.Repository.Interfaces
{
    public interface IContactService
    {
        Task<ContactResponseDto> CreateContactAsync(ContactRequestDto request);
        Task<ContactResponseDto> UpdateContactAsync(int id, ContactRequestDto request);
        Task<bool> DeleteContactAsync(int id);
        Task<ContactResponseDto> GetContactByIdAsync(int id);
        Task<List<ContactResponseDto>> GetAllContactsAsync();
    }
}
