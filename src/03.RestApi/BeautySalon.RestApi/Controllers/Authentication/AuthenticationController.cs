using BeautySalon.Application.Users.Contracts;
using BeautySalon.Application.Users.Contracts.Dtos;
using BeautySalon.Services.Users.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.Authentication;
[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserHandle _handle;

    public AuthenticationController(IUserHandle handle)
    {
        _handle = handle;
    }

    [HttpPost("login")]
    public async Task<GetTokenDto> Login([FromBody] LoginDto dto)
    {
        return await _handle.Login(dto);
    }

}
