using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services;
using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Services.Clients.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.Clients;
[Route("api/clients")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _service;
    private readonly IUserTokenService _userTokenService;
    public ClientsController(
        IClientService service,
        IUserTokenService userTokenService)
    {
        _service = service;
        _userTokenService = userTokenService;
    }

    [Authorize(Roles = SystemRole.Client)]
    [HttpGet]
    public async Task<IPageResult<GetAllClientAppointmentsDto>>
        GetClientAppointments(
        [FromQuery] Pagination? pagination = null,
        [FromQuery] ClientAppointmentFilterDto? filter = null)
    {
        var userId = _userTokenService.UserId;
        return await _service.GetClientAppointments(pagination, filter, userId);
    }
}
