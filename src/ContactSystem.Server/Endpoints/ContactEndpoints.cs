using ContactSystem.Application.Dtos;
using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Server.Endpoints;

public static class ContactEndpoints
{
    public static void MapContactEndpoints(this WebApplication app)
    {

        var contactGroup = app.MapGroup("contact")
            //.RequireAuthorization()
            .WithTags("Contact Management");

        contactGroup.MapPost("add-contact",
        async (ContactCreateDto contactCreateDto, IContactService contactService) =>
        {
            var result = await contactService.AddContactAsync(contactCreateDto);
            return Results.Ok(result);
        });

        contactGroup.MapDelete("delete-contact",
        async (long contactId, IContactService contactService) =>
        {

            await contactService.DeleteContactAsync(contactId);
            return Results.Ok();
        })
        .WithName("Delete Contact");


        contactGroup.MapPatch("update-contact",
        async (ContactDto contactDto, IContactService contactService) =>
        {
            await contactService.UpdateContactAsync(contactDto);
            return Results.Ok();
        })
        .WithName("Update Contact");


        contactGroup.MapGet("get-all-contacts",
        async ([FromQuery] int skip, [FromQuery] int take, IContactService contactService) =>
        {
            var page = new PageModel { Skip = skip, Take = take };
            var contacts = await contactService.GetAllContactsAsync(page);
            return Results.Ok(contacts);
        })
            .WithName("Get All Contacts");


        contactGroup.MapGet("get-contact-by-Id",
        async (long contactId, IContactService contactService) =>
        {
            var contactById = await contactService.GetContactByIdAsync(contactId);
            return Results.Ok(contactById);
        })
            .WithName("Get Contact By Id");
    }
}
