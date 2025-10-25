using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
public class AddWeeklyScheduleDto
{
    public List<Schedule> Schedules { get; set; } = new();
}

public class Schedule
{
    public DayWeek DayOfWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
