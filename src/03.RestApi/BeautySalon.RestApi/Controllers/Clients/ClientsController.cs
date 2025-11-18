using BeautySalon.Application.Clients.Contracts;
using BeautySalon.Application.Clients.Contracts.Dtos;
using BeautySalon.Application.Users.Contracts;
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
    private readonly ClientHandler _clientHandler;
    public ClientsController(
        IClientService service,
        IUserTokenService userTokenService,
        ClientHandler clientHandler)
    {
        _service = service;
        _userTokenService = userTokenService;
        _clientHandler = clientHandler;
    }

   

    [Authorize(Roles =SystemRole.Admin)]
    [HttpGet("all-for-create-appointment")]
    public async Task<List<GetAllClientsForAddAppointment>>
        GetAllForAppointment([FromQuery]string? search)
    {
        return await _service.GetAllForAppointment(search);
    }

    [HttpPost("new")]
    [Authorize(Roles =SystemRole.Admin)]
    public async Task<string> AddNewClient([FromBody]AddNewClientHandlerDto dto)
    {
        return await _clientHandler.AddNewClient(dto);
    }
}
