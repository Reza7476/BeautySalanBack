using BeautySalon.Application.Users.Contracts;
using BeautySalon.Application.Users.Contracts.Dtos;
using BeautySalon.Application.Users.Dtos;
using BeautySalon.Services.Users.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.Users;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserHandle _handle;

    public UsersController(IUserHandle handle)
    {
        _handle = handle;
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
}
