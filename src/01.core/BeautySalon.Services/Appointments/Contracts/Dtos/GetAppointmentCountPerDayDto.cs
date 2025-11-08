using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.Appointments.Contracts.Dtos;
public class GetAppointmentCountPerDayDto
{
    public DayWeek   DayWeek { get; set; }
    public int Count { get; set; }
}
