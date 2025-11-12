using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.Clients;
using BeautySalon.Entities.Roles;
using BeautySalon.Entities.Users;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Users;
public class EFUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly DbSet<UserRole> _userRoles;
    private readonly DbSet<Role> _roles;
    private readonly DbSet<Client> _clients;
    private readonly DbSet<Appointment> _appointments;

    public EFUserRepository(EFDataContext context)
    {
        _users = context.Set<User>();
        _userRoles = context.Set<UserRole>();
        _roles = context.Set<Role>();
        _clients = context.Set<Client>();
        _appointments = context.Set<Appointment>();
    }

    public async Task Add(User user)
    {
        await _users.AddAsync(user);
    }

    public async Task<User?> FindById(string id)
    {
        return await _users.Where(_ => _.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> FindByMobile(string mobile)
    {
        return await _users.FirstOrDefaultAsync(_ => _.Mobile == mobile);
    }

    public async Task<IPageResult<GetAllUsersDto>> GetAllUsers(
        IPagination? pagination,
        string? search)
    {
        var query = (from user in _users
                     join userRole in _userRoles on user.Id equals userRole.UserId into userRolesGroup

                     from userRoles in userRolesGroup.DefaultIfEmpty()
                     join role in _roles on userRoles.RoleId equals role.Id into roleGroup

                     from roles in roleGroup.DefaultIfEmpty()
                     join client in _clients on user.Id equals client.UserId into clientGroup

                     from client in clientGroup.DefaultIfEmpty()
                     select new GetAllUsersDto()
                     {
                         Name = user.Name,
                         LastName = user.LastName,
                         Mobile = user.Mobile,
                         Email = user.Email,
                         CreatedAt = user.CreationDate,
                         IsActive = user.IsActive,
                         UserName = user.UserName,
                         Roles = new List<string>() { roles.RoleName },
                         AppointmentNumber = _appointments.Where(_ => _.ClientId == client.Id).Count(),
                         Avatar = user.Avatar != null ? new ImageDetailsDto()
                         {
                             Extension = user.Avatar!.Extension!,
                             ImageName = user.Avatar!.ImageName!,
                             UniqueName = user.Avatar!.UniqueName!,
                             URL = user.Avatar!.URL!,
                         } : null,
                         Id = user.Id
                     }
                   ).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(_ => _.Mobile!.Contains(search));
        }

        query = query.OrderByDescending(_ => _.CreatedAt);


        return await query.Paginate(pagination ?? new Pagination());
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
                          IsActive = user.IsActive,
                          UserRoles = new List<string>() { role.RoleName }
                      }).FirstOrDefaultAsync();
    }

    public async Task<string?> GetUserIdByMobileNumber(string mobileNumber)
    {
        return await _users
            .Where(_ => _.Mobile == mobileNumber && _.IsActive)
            .Select(_ => _.Id).FirstOrDefaultAsync();
    }

    public async Task<GetUserInfoDto?> GetUserInfoById(string id)
    {
        var a = await (from user in _users
                       where user.Id == id
                       select new GetUserInfoDto()
                       {
                           Id = id,
                           Avatar = user.Avatar != null ? new ImageDetailsDto()
                           {
                               Extension = user.Avatar.Extension!,
                               ImageName = user.Avatar.ImageName!,
                               UniqueName = user.Avatar.UniqueName!,
                               URL = user.Avatar.URL!
                           } : null,
                           BirthDate = user.BirthDate,
                           CreationDate = user.CreationDate,
                           Email = user.Email,
                           IsActive = user.IsActive,
                           LastName = user.LastName,
                           Mobile = user.Mobile,
                           Name = user.Name,
                           UserName = user.UserName,
                           RoleNames = (from userRole in _userRoles
                                        join role in _roles
                                        on userRole.RoleId equals role.Id
                                        where userRole.UserId == user.Id
                                        select role.RoleName).ToList()
                       }).FirstOrDefaultAsync();
        return a;
    }

    public async Task<bool> IsExistByMobileNumber(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _users.AnyAsync(_ => _.UserName == userName);
    }

    public async Task<bool> IsExistByUserNameExceptItSelf(string userName, string id)
    {
        return await _users.AnyAsync(_ => _.UserName == userName && _.Id != id);
    }
}
