using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Interfaces;

public interface IContactRepository
{
    Task<long> InsertContactAsync(Contact contact);
    Task RemoveContactAsync(long contactId);
    Task<Contact> SelectContactByIdAsync(long contactId);
    Task<ICollection<Contact>> SelectAllContacts(PageModel pageModel);
    Task UpdateContactAsync(Contact contact);
}
