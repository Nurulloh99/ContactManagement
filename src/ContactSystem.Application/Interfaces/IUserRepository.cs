using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Interfaces;

public interface IUserRepository
{
    Task<long> InsertUserAsync(User user);
    Task<User> SelectUserByIdAsync(long userId);
    Task<User> SelectUserByUserNameAsync(string userName);
    Task UpdateUserAsync(User user);
    Task<IQueryable<User>> SelectAllUsersAsync(PageModel pageModel);

    Task UpdateUserByRoleAsync(long userId, string userRole);
    Task RemoveUserByIdAsync(long userId, string userRole);
}
