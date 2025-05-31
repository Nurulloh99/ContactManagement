using ContactSystem.Application.Dtos.Pagination;
using ContactSystem.Application.Interfaces;
using ContactSystem.Domain.Entities;
using ContactSystem.Errors;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext _appDbContext) : IUserRepository
{
    public async Task<long> InsertUserAsync(User user)
    {
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
        return user.UserId;
    }

    public Task RemoveUserByIdAsync(long userId, string userRole)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<User>> SelectAllUsersAsync(PageModel pageModel)
    {
        var users = _appDbContext.Users
            .AsNoTracking()
            .OrderBy(U => U.UserName)
            .Skip((pageModel.Skip - 1) * pageModel.Take)
            .Take(pageModel.Take);

        var query = users.ToQueryString();
        //_logger.LogInformation("Database query: " + query);

        return (IQueryable<User>)await users.ToListAsync();
    }

    public async Task<User> SelectUserByIdAsync(long userId)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

        if (user is null)
        {
            throw new EntityNotFoundException($"Not exists user with this ID: {userId} (Repository)");
        }

        return user;
    }

    public async Task<User> SelectUserByUserNameAsync(string userName)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        if (user is null)
        {
            throw new EntityNotFoundException($"Not exists user with this userName: {userName} (Repository)");
        }

        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        var existingContact = await _appDbContext.Contacts.FindAsync(user.UserId);
        if (existingContact != null)
        {
            _appDbContext.Entry(existingContact).State = EntityState.Detached;
        }

        _appDbContext.Users.Update(user);
        await _appDbContext.SaveChangesAsync();
    }

    public Task UpdateUserByRoleAsync(long userId, string userRole)
    {
        throw new NotImplementedException();
    }
}
