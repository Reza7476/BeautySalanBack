using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Appointments.Contracts.Dtos;
using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Services.Appointments.Contracts;
public interface IAppointmentService : IService
{
    Task<string> Add(AddAppointmentDto dto);

    Task CancelByClient(string appointmentId, string clientId);
    Task ChangeStatus(ChangeAppointmentStatusDto dto);

    Task<IPageResult<GetAllAdminAppointmentsDto>> GetAdminAllAppointments(
     IPagination? pagination = null,
     AdminAppointmentFilterDto? filter = null,
     string? search = null);

    Task<GetDashboardAdminSummaryDto?> GetAdminDashboardSummary();

    Task<IPageResult<GetAllAdminAppointmentsDto>> GetAllToday(
        IPagination? pagination = null,
        AdminAppointmentFilterDto? filter = null,
        string? search = null);

    Task<List<GetAppointmentCountPerDayDto>> GetAppointmentPerDayForChart();

    Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime);
 
    Task<IPageResult<GetAllClientAppointmentsDto>> GetClientAppointments(
        IPagination? pagination = null,
        ClientAppointmentFilterDto? filter = null,
        string? userId = null);
    
    Task<DashboardClientSummaryDto?> GetDashboardClientSummary(string userId);
    Task<GetAppointmentDetailsDto?> GetDetails(string id);
    Task<List<GetNewAppointmentsDashboardDto>> GetNewAppointmentDashboard();

    Task<IPageResult<GetAllAdminAppointmentsDto>> GetPendingAppointment(
     IPagination? pagination = null,
     AdminAppointmentFilterDto? filter = null,
     string? search = null);
}
