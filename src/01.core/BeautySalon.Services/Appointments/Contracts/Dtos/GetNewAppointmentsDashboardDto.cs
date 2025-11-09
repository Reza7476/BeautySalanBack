using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.Appointments.Contracts.Dtos;
public class GetNewAppointmentsDashboardDto
{
    public string? ClientName { get; set; }
    public string? ClientLastName { get; set; }
    public string? TreatmentTitle { get; set; }
    public string? Mobile { get; set; }
    public AppointmentStatus Status { get; set; }
    public DayWeek DayWeek { get; set; }
    public DateOnly Date { get; set; }
}
