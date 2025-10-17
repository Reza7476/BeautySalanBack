using BeautySalon.Application.Users.Contracts;
using BeautySalon.Application.Users.Dtos;
using BeautySalon.Entities.Users;
using BeautySalon.Services.JWTTokenService;
using BeautySalon.Services.RefreshTokens.Contacts;
using BeautySalon.Services.RefreshTokens.Exceptions;
using BeautySalon.Services.Roles.Contracts;
using BeautySalon.Services.Roles.Contracts.Dtos;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
using BeautySalon.Services.Users.Dtos;
using BeautySalon.Services.Users.Exceptions;

namespace BeautySalon.Application.Users;
public class UserCommandHandler : IUserHandle
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;


    public UserCommandHandler(IUserService userService,
        IRoleService roleService,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService)
    {
        _userService = userService;
        _roleService = roleService;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task EnsureAdminIsExist(string adminUser, string adminPass)
    {

        if (!await _userService.IsExistByUserName(adminUser))
        {
            var adminId = await CreateAdmin(adminUser, adminPass);
            var roleId = await CreateAdministratorAsRole();
            await AssignRoleToUser(adminId, roleId);
        }
    }

    public async Task<GetTokenDto> Login(LoginDto dto)
    {
        var user = await _userService.GetByUserNameForLogin(dto.UserName);

        if (user == null)
        {
            throw new UserNotFoundException();
        }
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.HashPass))
        {
            throw new UserNotFoundException();
        }
        var token = await _jwtTokenService.GenerateToken(user);
        var refreshToken = await _refreshTokenService.GenerateToken(user.Id!);

        return new GetTokenDto()
        {
            JwtToken = token,
            RefreshToken = refreshToken,
        };
    }

    public async Task<GetTokenDto> RefreshToken(string refreshToken)
    {
        var oldToken = await _refreshTokenService.GetTokenInfo(refreshToken);
        if (oldToken == null)
        {
            throw new TokenIsExpiredException();
        }

        if (oldToken.IsRevoked)
        {
            throw new TokenIsExpiredException();
        }
        var user = await _userService.GetByUserIdForRefreshToken(oldToken.UserId);

        var token = await _jwtTokenService.GenerateToken(user!);
        return new GetTokenDto()
        {
            JwtToken=token,
            RefreshToken = refreshToken,
        };
    }

    private async Task AssignRoleToUser(
        string adminId,
        long roleId)
    {
        await _roleService.AssignRoleToUser(adminId, roleId);
    }

    private async Task<string> CreateAdmin(string adminUser, string adminPass)
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
