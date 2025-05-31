using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Application.Interfaces;
using ContactSystem.Domain.Entities;
using ContactSystem.Errors;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure.Persistence.Repositories;

public class ContactRepository(AppDbContext _appDbContext) : IContactRepository
{
    public async Task RemoveContactAsync(long contactId)
    {
        var contact = await _appDbContext.Contacts.FirstOrDefaultAsync(c => c.ContactId == contactId);
        if (contact == null)
            throw new ArgumentNullException($"Contact not exists with this ID: {contact} (Repository)");
        _appDbContext.Remove(contact);

        _appDbContext.SaveChanges();
    }

    public async Task<long> InsertContactAsync(Contact contact)
    {
        await _appDbContext.AddAsync(contact);
        _appDbContext.SaveChanges();
        return contact.ContactId;
    }

    public async Task<ICollection<Contact>> SelectAllContacts(PageModel page)
    {
        var contacts = _appDbContext.Contacts
            .AsNoTracking()
            .OrderBy(c => c.ContactName)
            .Skip((page.Skip - 1) * page.Take)
            .Take(page.Take);

        var query = contacts.ToQueryString();
        //_logger.LogInformation("Database query: " + query);

        return await contacts.ToListAsync();
    }

    public async Task<Contact> SelectContactByIdAsync(long contactId)
    {
        var contact = await _appDbContext.Contacts.FirstOrDefaultAsync(c => c.ContactId == contactId);
        if (contact == null)
            throw new EntityNotFoundException($"Contact not exists with this ID: {contact} (Repository)");
        return contact;
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        var existingContact = await _appDbContext.Contacts.FindAsync(contact.ContactId);
        if (existingContact != null)
        {
            _appDbContext.Entry(existingContact).State = EntityState.Detached;
        }

        _appDbContext.Contacts.Update(contact);
        await _appDbContext.SaveChangesAsync();
    }
}
