using ContactSystem.Application.Dtos;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Services;

public static class MapService
{
    public static Contact ConvertToContactEntity(ContactCreateDto dto)
    {
        return new Contact
        {
            ContactName = dto.ContactName,
            ContactEmail = dto.ContactEmail,
            ContactPhoneNumber = dto.ContactPhoneNumber,
            ContactAddress = dto.ContactAddress,
        };
    }

    public static ContactDto ConvertToContactDto(Contact contact)
    {
        return new ContactDto
        {
            ContactId = contact.ContactId,
            ContactName = contact.ContactName,
            ContactEmail = contact.ContactEmail,
            ContactPhoneNumber = contact.ContactPhoneNumber,
            ContactAddress = contact.ContactAddress,
            CreatedAt = contact.CreatedAt,
        };
    }


    public static Contact ConvertToContactEntity(ContactDto dto)
    {
        return new Contact
        {
            ContactId = dto.ContactId,
            ContactName = dto.ContactName,
            ContactEmail = dto.ContactEmail,
            ContactPhoneNumber = dto.ContactPhoneNumber,
            ContactAddress = dto.ContactAddress,
            CreatedAt = dto.CreatedAt,
        };
    }

    //=======================================================================================================

    public static User ConvertToUserEntity(UserCreateDto dto)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Email = dto.Email,
            Password = dto.Password,
            PhoneNumber = dto.PhoneNumber
        };
    }

    public static UserGetDto ConvertToUserGetDto(User user)
    {
        return new UserGetDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
    }

    //=======================================================================================================

    public static UserRole ConvertToRoleEntity(RoleCreateDto dto)
    {
        return new UserRole
        {
            RoleName = dto.RoleName,
            RoleDescription = dto.RoleDescription
        };
    }

    public static RoleGetDto ConvertToRoleDto(UserRole role)
    {
        return new RoleGetDto
        {
            RoleName = role.RoleName,
            RoleDescription = role.RoleDescription
        };
    }
}
