using ContactSystem.Application.Dtos;
using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Application.Interfaces;
using ContactSystem.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ContactSystem.Application.Services;

public class UserService(IUserRepository _userRepository, ILogger<UserService> _logger) : IUserService
{
    public async Task<GetPageModel<UserGetDto>> GetAllUsersAsync(PageModel pageModel)
    {
        var users = await _userRepository.SelectAllUsersAsync(pageModel);

        var enUsers = new List<UserGetDto>();

        foreach (var user in users)
        {
            var dto = MapService.ConvertToUserGetDto(user);
            enUsers.Add(dto);
        }

        var result = new GetPageModel<UserGetDto>()
        {
            PageModel = pageModel,
            Items = enUsers,
            TotalCount = enUsers.Count()
        };

        return result;
    }

    public async Task<UserGetDto> GetUserByIdAsync(long userId)
    {
        var userById = await _userRepository.SelectUserByIdAsync(userId);
        var userDto = MapService.ConvertToUserGetDto(userById);

        return userDto;
    }

    public async Task<UserGetDto> GetUserByUserNameAsync(string userName)
    {
        var userById = await _userRepository.SelectUserByUserNameAsync(userName);
        var userDto = MapService.ConvertToUserGetDto(userById);

        return userDto;
    }

    public Task UpdateUserAsync(UserCreateDto updateDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserByRoleAsync(long userId, string userRole)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserByIdAsync(long userId, string userRole)
    {
        throw new NotImplementedException();
    }

    public async Task<long> AddUserAsync(UserCreateDto userCreateDto)
    {
        if (userCreateDto == null)
            throw new ArgumentNullException($"There is no user with this ID: {userCreateDto}");

        var user = MapService.ConvertToUserEntity(userCreateDto);
        user.UserId = 2;

        _logger.LogInformation($"Contact has been added successfully {DateTime.Now}");

        var result = await _userRepository.InsertUserAsync(user);
        return result;
    }
}
