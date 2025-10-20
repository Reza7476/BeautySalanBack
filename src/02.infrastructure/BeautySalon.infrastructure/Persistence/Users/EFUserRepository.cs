using BeautySalon.Entities.Roles;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Users;
public class EFUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly DbSet<UserRole> _userRoles;
    private readonly DbSet<Role> _roles;

    public EFUserRepository(EFDataContext context)
    {
        _users = context.Set<User>();
        _userRoles = context.Set<UserRole>();
        _roles = context.Set<Role>();

    }

    public async Task Add(User user)
    {
        await _users.AddAsync(user);
    }

    public async Task<User?> FindByMobile(string mobile)
    {
        return await _users.FirstOrDefaultAsync(_=>_.Mobile == mobile);    
    }

    public async Task<GetUserForLoginDto?> GetByUserIdForRefreshToken(string userId)
    {
        var a = await (from user in _users
                       join useRole in _userRoles
                       on user.Id equals useRole.UserId
                       join role in _roles
                       on useRole.RoleId equals role.Id
                       where user.Id == userId
                       select new GetUserForLoginDto()
                       {
                           UserName = user.UserName,
                           HashPass = user.HashPass,
                           Email = user.Email,
                           LastName = user.LastName,
                           Id = user.Id,
                           Mobile = user.Mobile,
                           Name = user.Name,
                           UserRoles = new List<string>() { role.RoleName }
                       }
                      ).FirstOrDefaultAsync();


        return a;
    }

    public async Task<GetUserForLoginDto?> GetByUserNameForLogin(string userName)
    {
        return await (from user in _users
                      join userRole in _userRoles
                      on user.Id equals userRole.UserId
                      join role in _roles
                      on userRole.RoleId equals role.Id
                      where user.UserName == userName
                      select new GetUserForLoginDto()
                      {
                          UserName = user.UserName,
                          HashPass = user.HashPass,
                          Email = user.Email,
                          LastName = user.LastName,
                          Id = user.Id,
                          Mobile = user.Mobile,
                          Name = user.Name,
                          UserRoles = new List<string>() { role.RoleName }
                      }).FirstOrDefaultAsync();
    }

    public async Task<string?> GetUserIdByMobileNumber(string mobileNumber)
    {
        return await _users
            .Where(_ => _.Mobile == mobileNumber && _.IsActive)
            .Select(_ => _.Id).FirstOrDefaultAsync();
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
