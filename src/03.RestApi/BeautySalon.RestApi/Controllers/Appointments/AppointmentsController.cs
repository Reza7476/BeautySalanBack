using BeautySalon.Application.Appointments.Contracts;
using BeautySalon.Application.Appointments.Contracts.Dtos;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services;
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


    [Authorize(Roles = SystemRole.Client)]
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

    [Authorize(Roles = SystemRole.Client)]
    [HttpPatch("{appointmentId}/cancel-by-client")]
    public async Task CancelByClient(string appointmentId)
    {
        var userId = _userTokenService.UserId;

        await _handler.CancelAppointmentByClient(appointmentId, userId);
    }

    [Authorize(Roles = SystemRole.Admin)]
    [HttpGet("all-admin")]
    public async Task<IPageResult<GetAllAdminAppointmentsDto>> GetAllAdminAppointment(
        [FromQuery] Pagination? pagination = null,
        [FromQuery] AdminAppointmentFilterDto? filter = null,
        [FromQuery]string? search = null)
    {
        return await _service.GetAdminAllAppointments(pagination, filter, search);
    }


    [Authorize(Roles = SystemRole.Admin)]
    [HttpGet("{id}")]
    public async Task<GetAppointmentDetailsDto?> GetDetails([FromRoute] string id)
    {
        return await _service.GetDetails(id);
    }

    [Authorize(Roles = SystemRole.Admin)]
    [HttpPatch("change-status")]
    public async Task ChangeStatus([FromBody] ChangeAppointmentStatusDto dto)
    {
        await _service.ChangeStatus(dto);
    }

    [Authorize(Roles = SystemRole.Admin)]
    [HttpPost("add-admin")]
    public async Task<string> AddAdminAppointment([FromBody] AddAdminAppointmentHandlerDto dto)
    {
        return await _handler.AddAdminAppointment(dto);
    }

    [Authorize(Roles = SystemRole.Admin)]
    [HttpGet("all-today")]
    public async Task<IPageResult<GetAllAdminAppointmentsDto>> GetAllToday(
        [FromQuery] Pagination? pagination = null,
        [FromQuery] AdminAppointmentFilterDto? filter = null,
        [FromQuery]string? search = null)
    {
        return await _service.GetAllToday(pagination, filter, search);
    }


    [HttpGet("appointment-per-day-for-chart")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task<List<GetAppointmentCountPerDayDto>> GetAppointmentPerDay()
    {
        return await _service.GetAppointmentPerDayForChart();
    }

    [HttpGet("admin-dashboard-summary")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task<GetDashboardAdminSummaryDto?> GetAdminDashboardSummary()
    {
        return await _service.GetAdminDashboardSummary();
    }


    [HttpGet("new-appointments-for-dashboard")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task<List<GetNewAppointmentsDashboardDto>> GetNewAppointmentDashboard()
    {
        return await _service.GetNewAppointmentDashboard();
    }

    [HttpGet("dashboard-client-summary")]
    [Authorize(Roles = SystemRole.Client)]
    public async Task<DashboardClientSummaryDto?> GetDashboardClientSummary()
    {
        var userId = _userTokenService.UserId;
        return await _service.GetDashboardClientSummary(userId!);
    }


    [HttpGet("all-pending-admin")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task<IPageResult<GetAllAdminAppointmentsDto>> GetPendingAppointment(
        [FromQuery] Pagination? pagination = null,
        [FromQuery] AdminAppointmentFilterDto? filter = null,
        [FromQuery] string? search = null)
    {
        return await _service.GetPendingAppointment(pagination, filter, search);
    }
}
