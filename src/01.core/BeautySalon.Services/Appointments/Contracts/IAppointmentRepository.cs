using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;

namespace BeautySalon.Services.Appointments.Contracts;
public interface IAppointmentRepository : IRepository
{
    Task Add(Appointment appointment);
    Task <bool>CheckStatusForNewAppointment(DateTime appointmentDate);
    Task<string?> GetClientIdByUserId(string userId);
    Task<string?> GetTechnicianId();
    Task<bool> TreatmentIsExistById(long treatmentId);
}
