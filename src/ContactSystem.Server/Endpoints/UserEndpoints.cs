using ContactSystem.Application.Dtos;
using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Server.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            var userGroup = app.MapGroup("user")
                //.RequireAuthorization()
                .WithTags("User Management");

            userGroup.MapPost("add-user",
            async (UserCreateDto userCreateDto, IUserService userService) =>
            {
                var result = await userService.AddUserAsync(userCreateDto);
                return Results.Ok(result);
            });

            //userGroup.MapDelete("delete",
            //async (long userId, HttpContext httpContext, IUserService userService) =>
            //{
            //    await userService.DeleteUserByIdAsync(userId);
            //    return Results.Ok();
            //})
            //.WithName("Delete User");


            userGroup.MapPatch("update",
            async (UserCreateDto userDto, IUserService userService) =>
            {
                await userService.UpdateUserAsync(userDto);
                return Results.Ok();
            })
            .WithName("Update User");


            userGroup.MapGet("getAll",
            async ([FromBody] PageModel pageModel, IUserService userService) =>
            {
                var users = await userService.GetAllUsersAsync(pageModel);
                return Results.Ok(users);
            })
                .WithName("Get All Users");


            userGroup.MapGet("getById",
            async (long userID, IUserService userService) =>
            {
                var userById = await userService.GetUserByIdAsync(userID);
                return Results.Ok(userById);
            })
                .WithName("Get User By Id");
        }
    }
}
