using ContactSystem.Application.Dtos;
using ContactSystem.Application.Interfaces;

namespace ContactSystem.Application.Services;

public class UserRoleService(IUserRoleRepository _userRoleRepository) : IUserRoleService
{
    public Task<long> AddRoleAsync(UserRoleDto role)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRoleAsync(long roleId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RoleGetDto>> GetAllRolesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetRoleIdAsync(string role)
    {
        throw new NotImplementedException();
    }
}
