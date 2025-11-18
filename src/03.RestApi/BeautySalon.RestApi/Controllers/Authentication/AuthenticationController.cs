using BeautySalon.Application.Users.Contracts;
using BeautySalon.Application.Users.Contracts.Dtos;
using BeautySalon.Services.RefreshTokens.Contacts;
using BeautySalon.Services.RefreshTokens.Contacts.Dtos;
using BeautySalon.Services.Users.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.Authentication;
[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserHandle _handle;
    private readonly IRefreshTokenService _refreshTokenService;


    public AuthenticationController(
        IUserHandle handle,
        IRefreshTokenService refreshTokenService)
    {
        _handle = handle;
        _refreshTokenService = refreshTokenService;
    }

    [HttpPost("login")]
    public async Task<GetTokenDto> Login([FromBody] LoginDto dto)
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
    public async Task<GetTokenDto> RefreshToken([FromRoute]string refreshToken)
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
}
