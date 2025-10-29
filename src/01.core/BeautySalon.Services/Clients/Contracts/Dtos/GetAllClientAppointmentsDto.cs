using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.Clients.Contracts.Dtos;
public class GetAllClientAppointmentsDto
{
    public string Id { get; set; } = default!;
    public string TreatmentTitle { get; set; } = default!;
    public int Duration { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayWeek DayWeek { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public DateOnly CreatedAt { get; set; }
    public string? CancelledBy { get; set; }
    public DateOnly CancelledDate { get; set; }
}
