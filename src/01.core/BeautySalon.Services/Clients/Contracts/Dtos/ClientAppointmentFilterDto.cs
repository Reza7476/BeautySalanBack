using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.Clients.Contracts.Dtos;
public class ClientAppointmentFilterDto
{
    public DayWeek Day { get; set; }
    public DateOnly Date { get; set; }
    public AppointmentStatus Status { get; set; }

}
