using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
public class AddWeeklyScheduleDto
{
    public DayWeek DayOfWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
}
