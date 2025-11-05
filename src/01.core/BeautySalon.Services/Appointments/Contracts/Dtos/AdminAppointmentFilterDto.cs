using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.Appointments.Contracts.Dtos;
public class AdminAppointmentFilterDto
{
    public DayWeek Day { get; set; }
    public DateOnly Date { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? TreatmentTitle { get; set; }
}
