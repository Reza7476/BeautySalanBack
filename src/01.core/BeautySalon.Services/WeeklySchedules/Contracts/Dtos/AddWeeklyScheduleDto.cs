namespace BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
public class AddWeeklyScheduleDto
{
    public List<ScheduleDto> Schedules { get; set; } = new();
}
