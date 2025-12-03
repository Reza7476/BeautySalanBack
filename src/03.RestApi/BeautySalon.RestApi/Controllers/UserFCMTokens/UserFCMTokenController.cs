using BeautySalon.Common.Interfaces;
using BeautySalon.Services.UserFCMTokens.Contract;
using BeautySalon.Services.UserFCMTokens.Contract.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.UserFCMTokens;
[Route("api/user-fcm-token")]
[ApiController]
public class UserFCMTokenController : ControllerBase
{
    private readonly IUserFCMTokenService _service;
    private readonly IUserTokenService _userTokenService;


    public UserFCMTokenController(
        IUserFCMTokenService service,
        IUserTokenService userTokenService)
    {
        _service = service;
        _userTokenService = userTokenService;
    }

    [HttpPost]
    [Authorize]
    public async Task Add([FromBody] AddUserFCMTokenDto dto)
    {
        var userId = _userTokenService.UserId;
        await _service.Add(dto, userId!);
    }
}
