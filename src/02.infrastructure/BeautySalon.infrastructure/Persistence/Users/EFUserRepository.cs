using BeautySalon.Entities.Users;
using BeautySalon.Services.Users.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Users;
public class EFUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public EFUserRepository(EFDataContext context)
    {
        _users = context.Set<User>();
    }

    public async Task Add(User user)
    {
        await _users.AddAsync(user);
    }

    public async Task<bool> IsExistByMobileNumber(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _users.AnyAsync(_ => _.UserName == userName);
    }
}
