using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Test.Tool.Entities.WeeklySchedules;
public class WeeklyScheduleBuilder
{
    private readonly WeeklySchedule _schedule;

    public WeeklyScheduleBuilder()
    {
        _schedule = new WeeklySchedule()
        {
            DayOfWeek = DayWeek.Tuesday,
            IsActive = true,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(8),
        };
    }

    public WeeklyScheduleBuilder WithDay(DayWeek day)
    {
        _schedule.DayOfWeek = day;
        return this;
    }

    public WeeklyScheduleBuilder WithStartTime(DateTime time)
    {
        _schedule.StartTime = time;
        return this;
    }

    public WeeklyScheduleBuilder WithEndTime(DateTime time)
    {
        _schedule.EndTime = time;
        return this;
    }

    public WeeklyScheduleBuilder WithIsActive(bool active)
    {
        _schedule.IsActive = active;
        return this;
    }

    public WeeklySchedule Build()
    {
        return _schedule;
    }
}
