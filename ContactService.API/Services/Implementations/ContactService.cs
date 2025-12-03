using AutoMapper;
using ContactServices.API.DTOs;
using ContactServices.API.Models;
using ContactServices.API.Repository.Interfaces;

namespace ContactServices.API.Repository.Implementations
{
    public class ContactServices: IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactServices(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateContactDto> CreateContactAsync(CreateContactDto request)
        {
            var entity = _mapper.Map<Contact>(request);

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<CreateContactDto>(entity);
        }

        public async Task<CreateContactDto> UpdateContactAsync(int id, CreateContactDto request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Contact not found!");

            _mapper.Map(request, existing);

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return _mapper.Map<CreateContactDto>(existing);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<ContactResponseDto> GetContactByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ContactResponseDto>(entity);
        }

        public async Task<List<ContactResponseDto>> GetAllContactsAsync()
        {
            var contacts = await _repository.GetAllAsync();
            return _mapper.Map<List<ContactResponseDto>>(contacts);
        }
    }
}
