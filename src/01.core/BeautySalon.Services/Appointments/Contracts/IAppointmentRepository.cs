using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts.Dtos;
using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Services.Appointments.Contracts;
public interface IAppointmentRepository : IRepository
{
    Task Add(Appointment appointment);
    Task<bool> CheckStatusForNewAppointment(DateTime appointmentDate);
    Task<Appointment?> FindById(string id);
    Task<Appointment?> FindByIdAndClientId(string appointmentId, string clientId);

    Task<IPageResult<GetAllAdminAppointmentsDto>>
        GetAdminAllAppointments(
        IPagination? pagination,
        AdminAppointmentFilterDto? filter,
        string? search);

    Task<GetDashboardAdminSummaryDto?> GetAdminDashboardSummary();

    Task<IPageResult<GetAllAdminAppointmentsDto>> GetAllToday(
        IPagination? pagination = null,
        AdminAppointmentFilterDto? filter = null,
        string? search = null);
    Task<List<GetAppointmentCountPerDayDto>> GetAppointmentPerDayForChart();
    
    Task<List<GetAppointmentRequiringSMSDto>> GetAppointmentRequiringSMS();

    Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime);
    Task<List<Appointment>> GetByIds(List<string> listIds);
    Task<IPageResult<GetAllClientAppointmentsDto>> GetClientAppointments(
        string clientId,
        IPagination? pagination = null,
        ClientAppointmentFilterDto? filter = null);

    Task<string?> GetClientIdByUserId(string userId);
    Task<DashboardClientSummaryDto?> GetDashboardClientSummary(string userId);
    Task<GetAppointmentDetailsDto?> GetDetails(string id);
    Task<List<GetNewAppointmentsDashboardDto>> GetNewAppointmentDashboard();
    
    Task<List<Appointment>> GetOverdueUnfinalizedAppointments();
    
    Task<IPageResult<GetAllAdminAppointmentsDto>> GetPendingAppointment(
        IPagination? pagination = null,
        AdminAppointmentFilterDto? filter = null,
        string? search = null);

    Task<string?> GetTechnicianId();
    Task<bool> TreatmentIsExistById(long treatmentId);
}
