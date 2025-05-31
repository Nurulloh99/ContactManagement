using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Application.Interfaces;
using ContactSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure.Persistence.Repositories;

public class UserRoleRepository(AppDbContext _appDbContext) : IUserRoleRepository
{
    public Task DeleteRoleAsync(long roleId)
    {
        throw new NotImplementedException();
    }

    public Task<long> InsertRoleAsync(UserRole role)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserRole>> SelectAllRolesAsync(PageModel pageModel)
    {
        var roles = _appDbContext.UserRoles
            .AsNoTracking()
            .OrderBy(r => r.RoleName)
            .Skip((pageModel.Skip - 1) * pageModel.Take)
            .Take(pageModel.Take);

        var query = roles.ToQueryString();
        //_logger.LogInformation("Database query: " + query);

        return await roles.ToListAsync();
    }

    public Task<ICollection<User>> SelectAllUsersByRoleAsync(string role)
    {
        throw new NotImplementedException();
    }

    public Task<long> SelectRoleIdAsync(string role)
    {
        throw new NotImplementedException();
    }
}
