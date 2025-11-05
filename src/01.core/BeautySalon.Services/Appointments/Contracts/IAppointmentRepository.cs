using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts.Dtos;

namespace BeautySalon.Services.Appointments.Contracts;
public interface IAppointmentRepository : IRepository
{
    Task Add(Appointment appointment);
    Task<bool> CheckStatusForNewAppointment(DateTime appointmentDate);
    Task<Appointment?> FindByIdAndClientId(string appointmentId, string clientId);
   
    Task<IPageResult<GetAllAdminAppointmentsDto>> 
        GetAdminAllAppointments(
        IPagination? pagination,
        AdminAppointmentFilterDto? filter,
        string? search);
    
    Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime);

    Task<string?> GetClientIdByUserId(string userId);
    Task<string?> GetTechnicianId();
    Task<bool> TreatmentIsExistById(long treatmentId);
}
