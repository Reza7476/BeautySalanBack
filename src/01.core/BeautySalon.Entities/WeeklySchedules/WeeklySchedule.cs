namespace BeautySalon.Entities.WeeklySchedules;
public class WeeklySchedule
{
    public int Id { get; set; }
    public DayWeek DayOfWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
}

public enum DayWeek : byte
{
    None=0,
    Saturday = 1,
    Sunday = 2,
    Monday = 3,
    Tuesday = 4,
    Wednesday = 5,
    Thursday = 6,
    Friday = 7,
}
