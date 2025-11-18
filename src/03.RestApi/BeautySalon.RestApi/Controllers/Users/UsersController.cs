using BeautySalon.Application.Users.Contracts;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services;
using BeautySalon.Services.RefreshTokens.Contacts;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.Users;
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserHandle _handle;
    private readonly IUserTokenService _userTokenService;
    private readonly IUserService _userService;


    public UsersController(
        IUserHandle handle,
        IRefreshTokenService refreshTokenService,
        IUserTokenService userTokenService,
        IUserService userService)
    {
        _handle = handle;
        _userTokenService = userTokenService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<GetUserInfoDto?> GetUserInfo()
    {
        var userId = _userTokenService.UserId;
        return await _userService.GetUserInfo(userId);
    }

    [Authorize(Roles = SystemRole.Admin)]
    [HttpPatch("admin-profile")]
    public async Task EditAdminProfile([FromBody] EditAdminProfileDto dto)
    {
        var userId = _userTokenService.UserId;
        await _userService.EditAdminProfile(dto, userId);
    }

    [Authorize]
    [HttpPatch("profile-image")]
    public async Task EditProfileImage([FromForm] AddMediaDto dto)
    {
        var userId = _userTokenService.UserId;
        await _handle.EditProfileImage(dto, userId!);
    }

    [HttpPatch("client-profile")]
    [Authorize(Roles = SystemRole.Client)]
    public async Task EditClientProfile([FromBody] EditClientProfileDto dto)
    {
        var userId = _userTokenService.UserId;
        await _userService.EditClientProfile(dto, userId!);
    }

    [HttpGet("all")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task<IPageResult<GetAllUsersDto>> GetAllUsers(
        [FromQuery] Pagination? pagination = null,
        string? search = null)
    {
        return await _userService.GetAllUsers(pagination, search);
    }

    [HttpPatch("change-user-activation")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task ChangeUserActivation([FromBody] ChangeUserActivationDto dto)
    {
        var userId = _userTokenService.UserId;
        await _userService.ChangeUserActivation(dto, userId!);
    }
}
