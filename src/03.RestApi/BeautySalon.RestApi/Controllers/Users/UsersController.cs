using BeautySalon.Application.Users.Contracts;
using BeautySalon.Application.Users.Dtos;
using BeautySalon.Services.Users.Contracts.Dtos;
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
    public async Task<GetTokenDto> Login([FromForm]LoginDto dto)
    {
        return await _handle.Login(dto);
    }
}
