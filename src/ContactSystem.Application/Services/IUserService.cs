using ContactSystem.Application.Dtos;
using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Services;

public interface IUserService
{
    Task<long> AddUserAsync(UserCreateDto userCreateDto);
    Task<UserGetDto> GetUserByIdAsync(long userId);
    Task<UserGetDto> GetUserByUserNameAsync(string userName);
    Task<GetPageModel<UserGetDto>> GetAllUsersAsync(PageModel pageModel);
    Task UpdateUserAsync(UserCreateDto updateDto);


    Task UpdateUserByRoleAsync(long userId, string userRole);
    Task DeleteUserByIdAsync(long userId, string userRole);
}
