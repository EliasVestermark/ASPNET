using ASPNetAPI.Models;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace ASPNetAPI.Services
{
    public class ContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateContact(ContactFormModel model)
        {
            try
            {
                var contactEntity = new ContactEntity
                {
                    Email = model.Email,
                    Service = model.Service,
                    Message = $"{model.Message}, sent by {model.FullName}",
                };

                _context.Contacts.Add(contactEntity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
