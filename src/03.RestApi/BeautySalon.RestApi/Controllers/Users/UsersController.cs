using BeautySalon.Application.Users.Contracts;
using BeautySalon.Application.Users.Contracts.Dtos;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services;
using BeautySalon.Services.RefreshTokens.Contacts;
using BeautySalon.Services.RefreshTokens.Contacts.Dtos;
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
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUserTokenService _userTokenService;
    private readonly IUserService _userService;


    public UsersController(
        IUserHandle handle,
        IRefreshTokenService refreshTokenService,
        IUserTokenService userTokenService,
        IUserService userService)
    {
        _handle = handle;
        _refreshTokenService = refreshTokenService;
        _userTokenService = userTokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<GetTokenDto> Login([FromForm] LoginDto dto)
    {
        return await _handle.Login(dto);
    }

    [HttpPost("initializing-register-user")]
    public async Task<ResponseInitializeRegisterUserHandlerDto> InitializingRegister(
        [FromBody] InitializeRegisterUserDto dto)
    {
        return await _handle.InitializeRegister(dto);
    }

    [HttpPost("finalizing-register-user")]
    public async Task<GetTokenDto> FinalizingRegister(
        [FromBody] FinalizingRegisterUserHandlerDto dto)
    {
        return await _handle.FinalizingRegister(dto);
    }

    [Authorize]
    [HttpPost("{refreshToken}/refresh-token")]
    public async Task<GetTokenDto> RefreshToken(string refreshToken)
    {
        return await _handle.RefreshToken(refreshToken);
    }


    [HttpPost("forget-pass-step-one")]
    public async Task<ResponseInitializeRegisterUserHandlerDto>
        ForgetPassword([FromBody] InitializeRegisterUserDto dto)
    {
        return await _handle.ForgetPasswordInitialize(dto);
    }

    [HttpPost("forget-password-step-two")]
    public async Task ForgetPassStepTwo([FromBody] ForgetPassStepTwoDto dto)
    {
        await _handle.FinalizeResetPassword(dto);
    }

    [HttpPatch("log-out")]
    public async Task Logout([FromBody] LogOutDto dto)
    {
        await _refreshTokenService.RevokedToken(dto);
    }

    [Authorize]
    [HttpGet]
    public async Task<GetUserInfoDto?> GetUserInfo()
    {
        var userId = _userTokenService.UserId;
        return await _userService.GetUserInfo(userId);
    }

    [Authorize(Roles =SystemRole.Admin)]
    [HttpPatch("admin-profile")]
    public async Task EditAdminProfile([FromBody]EditAdminProfileDto dto)
    {
        var userId=_userTokenService.UserId;
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
    [Authorize(Roles =SystemRole.Client)]
    public async Task EditClientProfile([FromBody] EditClientProfileDto dto)
    {
        var userId = _userTokenService.UserId;
        await _userService.EditClientProfile(dto, userId!);
    }
}
