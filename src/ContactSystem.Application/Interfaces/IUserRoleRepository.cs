using ContactSystem.Application.Dtos;
using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Interfaces;

public interface IUserRoleRepository
{
    Task<long> InsertRoleAsync(UserRole role);
    Task DeleteRoleAsync(long roleId);
    Task<ICollection<User>> SelectAllUsersByRoleAsync(string role);
    Task<List<UserRole>> SelectAllRolesAsync(PageModel pageModel);
    Task<long> SelectRoleIdAsync(string role);
}
