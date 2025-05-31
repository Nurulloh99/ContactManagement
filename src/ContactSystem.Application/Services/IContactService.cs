using ContactSystem.Application.Dtos;
using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Services;

public interface IContactService
{
    Task<long> AddContactAsync(ContactCreateDto contactCreateDto);
    Task DeleteContactAsync(long contactId);
    Task<ContactDto> GetContactByIdAsync(long contactId);
    Task<GetPageModel<ContactDto>> GetAllContactsAsync(PageModel page);
    Task UpdateContactAsync(ContactDto contactDto);
}
