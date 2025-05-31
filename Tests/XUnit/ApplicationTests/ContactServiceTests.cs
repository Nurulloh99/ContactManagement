using ContactSystem.Application.Dtos;
using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Application.Interfaces;
using ContactSystem.Application.Services;
using ContactSystem.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly Mock<ILogger<ContactService>> _loggerMock;

    public ContactServiceTests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _loggerMock = new Mock<ILogger<ContactService>>();
    }

    [Fact]
    public async Task AddContactAsync_ValidInput_ReturnsContactId()
    {
        // Arrange
        var dto = new ContactCreateDto
        {
            ContactName = "John",
            ContactEmail = "john@example.com",
            ContactPhoneNumber = "1234567890",
            ContactAddress = "123 Main St"
        };

        var contactEntity = new Contact
        {
            ContactName = dto.ContactName,
            ContactEmail = dto.ContactEmail,
            ContactPhoneNumber = dto.ContactPhoneNumber,
            ContactAddress = dto.ContactAddress,
            UserId = 2
        };

        // Mock MapService
        MapServiceMock.SetupConvertToContactEntity(dto, contactEntity);

        _contactRepositoryMock
            .Setup(r => r.InsertContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(42);

        var service = new ContactService(_contactRepositoryMock.Object, _loggerMock.Object);

        // Act
        var result = await service.AddContactAsync(dto);

        // Assert
        Assert.Equal(42, result);
        _contactRepositoryMock.Verify(r => r.InsertContactAsync(It.Is<Contact>(c => c.UserId == 2)), Times.Once);
    }

    [Fact]
    public async Task AddContactAsync_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var service = new ContactService(_contactRepositoryMock.Object, _loggerMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddContactAsync(null));
    }

    [Fact]
    public async Task DeleteContactAsync_ValidId_CallsRepository()
    {
        // Arrange
        var service = new ContactService(_contactRepositoryMock.Object, _loggerMock.Object);

        // Act
        await service.DeleteContactAsync(1);

        // Assert
        _contactRepositoryMock.Verify(r => r.RemoveContactAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetAllContactsAsync_ReturnsPagedContacts()
    {
        // Arrange
        var pageModel = new PageModel { Skip = 0, Take = 10 };
        var contacts = new List<Contact>
        {
            new Contact { ContactId = 1, ContactName = "A", ContactEmail = "a@a.com", ContactPhoneNumber = "1", ContactAddress = "Addr", CreatedAt = DateTime.UtcNow },
            new Contact { ContactId = 2, ContactName = "B", ContactEmail = "b@b.com", ContactPhoneNumber = "2", ContactAddress = "Addr2", CreatedAt = DateTime.UtcNow }
        };

        _contactRepositoryMock
            .Setup(r => r.SelectAllContacts(pageModel))
            .ReturnsAsync(contacts);

        // Mock MapService
        MapServiceMock.SetupConvertToContactDto(contacts[0], new ContactDto
        {
            ContactId = 1,
            ContactName = "A",
            ContactEmail = "a@a.com",
            ContactPhoneNumber = "1",
            ContactAddress = "Addr",
            CreatedAt = contacts[0].CreatedAt
        });
        MapServiceMock.SetupConvertToContactDto(contacts[1], new ContactDto
        {
            ContactId = 2,
            ContactName = "B",
            ContactEmail = "b@b.com",
            ContactPhoneNumber = "2",
            ContactAddress = "Addr2",
            CreatedAt = contacts[1].CreatedAt
        });

        var service = new ContactService(_contactRepositoryMock.Object, _loggerMock.Object);

        // Act
        var result = await service.GetAllContactsAsync(pageModel);

        // Assert
        Assert.Equal(2, result.TotalCount);
        Assert.Collection(result.Items,
            item => Assert.Equal(1, item.ContactId),
            item => Assert.Equal(2, item.ContactId));
    }

    [Fact]
    public async Task GetContactByIdAsync_ReturnsContactDto()
    {
        // Arrange
        var contact = new Contact
        {
            ContactId = 1,
            ContactName = "A",
            ContactEmail = "a@a.com",
            ContactPhoneNumber = "1",
            ContactAddress = "Addr",
            CreatedAt = DateTime.UtcNow
        };

        _contactRepositoryMock
            .Setup(r => r.SelectContactByIdAsync(1))
            .ReturnsAsync(contact);

        var contactDto = new ContactDto
        {
            ContactId = 1,
            ContactName = "A",
            ContactEmail = "a@a.com",
            ContactPhoneNumber = "1",
            ContactAddress = "Addr",
            CreatedAt = contact.CreatedAt
        };

        MapServiceMock.SetupConvertToContactDto(contact, contactDto);

        var service = new ContactService(_contactRepositoryMock.Object, _loggerMock.Object);

        // Act
        var result = await service.GetContactByIdAsync(1);

        // Assert
        Assert.Equal(contactDto.ContactId, result.ContactId);
        Assert.Equal(contactDto.ContactName, result.ContactName);
    }

    [Fact]
    public async Task UpdateContactAsync_ValidInput_CallsRepository()
    {
        // Arrange
        var dto = new ContactDto
        {
            ContactId = 1,
            ContactName = "A",
            ContactEmail = "a@a.com",
            ContactPhoneNumber = "1",
            ContactAddress = "Addr",
            CreatedAt = DateTime.UtcNow
        };

        var contactEntity = new Contact
        {
            ContactId = 1,
            ContactName = "A",
            ContactEmail = "a@a.com",
            ContactPhoneNumber = "1",
            ContactAddress = "Addr",
            CreatedAt = dto.CreatedAt,
            UserId = 2
        };

        MapServiceMock.SetupConvertToContactEntity(dto, contactEntity);

        var service = new ContactService(_contactRepositoryMock.Object, _loggerMock.Object);

        // Act
        await service.UpdateContactAsync(dto);

        // Assert
        _contactRepositoryMock.Verify(r => r.UpdateContactAsync(It.Is<Contact>(c => c.ContactId == 1 && c.UserId == 2)), Times.Once);
    }

    [Fact]
    public async Task UpdateContactAsync_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var service = new ContactService(_contactRepositoryMock.Object, _loggerMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateContactAsync(null));
    }
}

// Helper static class to mock MapService static methods
public static class MapServiceMock
{
    public static void SetupConvertToContactEntity(ContactCreateDto dto, Contact entity)
    {
        // Replace with your static mocking framework or wrap MapService for testability
        // For demonstration, assume MapService.ConvertToContactEntity returns the provided entity
        entity = MapService.ConvertToContactEntity(dto);
    }

    public static void SetupConvertToContactEntity(ContactDto dto, Contact entity)
    {
        entity = MapService.ConvertToContactEntity(dto);
    }

    public static void SetupConvertToContactDto(Contact entity, ContactDto dto)
    {
        dto = MapService.ConvertToContactDto(entity);
    }
}