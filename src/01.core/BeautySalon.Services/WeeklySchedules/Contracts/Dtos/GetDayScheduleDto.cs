namespace BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
public class GetDayScheduleDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
}
