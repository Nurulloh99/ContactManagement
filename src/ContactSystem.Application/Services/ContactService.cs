using ContactSystem.Application.Dtos;
using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Application.Interfaces;
using ContactSystem.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ContactSystem.Application.Services;

public class ContactService(IContactRepository _contactRepository, ILogger<ContactService> _logger) : IContactService
{
    public async Task<long> AddContactAsync(ContactCreateDto contactCreateDto)
    {
        if (contactCreateDto == null)
            throw new ArgumentNullException($"There is no contact with this ID: {contactCreateDto}");

        var contact = MapService.ConvertToContactEntity(contactCreateDto);
        contact.UserId = 2;

        _logger.LogInformation($"Contact has been added successfully {DateTime.Now}");

        var result = await _contactRepository.InsertContactAsync(contact);
        return result;
    }

    public async Task DeleteContactAsync(long contactId)
    {
        await _contactRepository.RemoveContactAsync(contactId);
    }

    public async Task<GetPageModel<ContactDto>> GetAllContactsAsync(PageModel page)
    {
        var contacts = await _contactRepository.SelectAllContacts(page);

        var enContacts = new List<ContactDto>();

        foreach (var contact in contacts)
        {
            var dto = MapService.ConvertToContactDto(contact);
            enContacts.Add(dto);
        }

        var result = new GetPageModel<ContactDto>()
        {
            PageModel = page,
            Items = enContacts,
            TotalCount = enContacts.Count()
        };

        return result;
    }

    public async Task<ContactDto> GetContactByIdAsync(long contactId)
    {
        var contactById = await _contactRepository.SelectContactByIdAsync(contactId);
        var contactDto = MapService.ConvertToContactDto(contactById);

        return contactDto;
    }

    public async Task UpdateContactAsync(ContactDto contactDto)
    {
        if (contactDto == null)
            throw new ArgumentNullException($"Not exists this contact {contactDto} (Service)");

        var enContact = MapService.ConvertToContactEntity(contactDto);
        enContact.UserId = 2;

        await _contactRepository.UpdateContactAsync(enContact);
    }



}
