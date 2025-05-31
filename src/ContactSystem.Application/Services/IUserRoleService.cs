using ContactSystem.Application.Dtos;

namespace ContactSystem.Application.Services;

public interface IUserRoleService
{
    Task<long> AddRoleAsync(UserRoleDto role);
    Task DeleteRoleAsync(long roleId);
    Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role);
    Task<List<RoleGetDto>> GetAllRolesAsync();
    Task<long> GetRoleIdAsync(string role);

}
 