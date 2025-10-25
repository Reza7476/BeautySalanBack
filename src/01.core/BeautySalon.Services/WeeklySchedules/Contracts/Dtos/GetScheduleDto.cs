using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
public class GetScheduleDto
{
    public bool IsActive { get; set; }
    public DayWeek DayOfWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Id { get; set; }
}
