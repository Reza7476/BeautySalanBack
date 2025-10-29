using BeautySalon.Application.Appointments.Contracts;
using BeautySalon.Application.Appointments.Contracts.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Services.Appointments.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.Appointments;
[Route("api/appointments")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _service;
    private readonly IUserTokenService _userTokenService;
    private readonly IAppointmentHandler _handler;

    public AppointmentsController(
        IAppointmentService service,
        IUserTokenService userTokenService,
        IAppointmentHandler handler)
    {
        _service = service;
        _userTokenService = userTokenService;
        _handler = handler;
    }


    [Authorize(Roles = "Client")]
    [HttpPost]
    public async Task<string> Add([FromBody] AddAppointmentHandlerDto dto)
    {
        var userId = _userTokenService.UserId;
        return await _handler.AddAppointment(dto, userId!);
    }



    [Authorize]
    [HttpGet("booked-appointment")]
    public async Task<List<GetBookedAppointmentByDayDto>>
        GetBookedAppointment([FromForm] DateTime date)
    {
        return await _service.GetBookAppointmentByDay(date);
    } 
}
