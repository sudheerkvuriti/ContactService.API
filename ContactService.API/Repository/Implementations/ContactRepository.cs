using ContactServices.API.Data;
using ContactServices.API.Models;
using ContactServices.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactServices.API.Repository.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;

        public ContactRepository(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await GetByIdAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
