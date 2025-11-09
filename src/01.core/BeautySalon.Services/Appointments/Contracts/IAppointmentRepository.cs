using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts.Dtos;

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
    Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime);

    Task<string?> GetClientIdByUserId(string userId);
    Task<GetAppointmentDetailsDto?> GetDetails(string id);
    Task<List<GetNewAppointmentsDashboardDto>> GetNewAppointmentDashboard();
    Task<string?> GetTechnicianId();
    Task<bool> TreatmentIsExistById(long treatmentId);
}
