using BeautySalon.Application.Users.Contracts;
using BeautySalon.Services.Roles.Contracts;
using BeautySalon.Services.Roles.Contracts.Dtos;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Dtos;

namespace BeautySalon.Application.Users;
public class UserCommandHandler : IUserHandle
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    public UserCommandHandler(IUserService userService,
        IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    public async Task EnsureAdminIsExist(string adminUser, string adminPass)
    {

        if (!await _userService.IsExistByUserName(adminUser))
        {
           var adminId= await CreateAdmin(adminUser,adminPass);
            var roleId =await CreateAdministratorAsRole();
            await AssignRoleToUser(adminId, roleId);
        }
    }

    private async Task AssignRoleToUser(
        string adminId,
        long roleId)
    {
        await _roleService.AssignRoleToUser(adminId, roleId);
    }

    private async Task<string> CreateAdmin(string adminUser,string adminPass)
    {
        var userId = await _userService.Add(new AddUserDto()
        {
            Name = "Sahar",
            LastName = "Dehghani",
            Email = "example@Gmail.com",
            Mobile = "09174367476",
            UserName = adminUser,
            Password = adminPass
        });
        return userId;
    }

    private async Task<long> CreateAdministratorAsRole()
    {
        var roleDto = new AddRoleDto()
        {
            RoleName = "Admin",
        };

        var roleId = await _roleService.Add(roleDto);
        return roleId;
    }
}
