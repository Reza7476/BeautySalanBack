using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Appointments.Contracts.Dtos;

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
    
    Task<IPageResult<GetAllAdminAppointmentsDto>> GetAllToday(
        IPagination? pagination=null,
        AdminAppointmentFilterDto? filter = null,
        string? search=null);
    
    Task<List<GetAppointmentCountPerDayDto>> GetAppointmentPerDayForChart();

    Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime);
    Task<GetAppointmentDetailsDto?> GetDetails(string id);
}
