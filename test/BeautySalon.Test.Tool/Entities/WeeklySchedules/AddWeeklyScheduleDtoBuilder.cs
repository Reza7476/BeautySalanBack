using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.WeeklySchedules;
public class AddWeeklyScheduleDtoBuilder
{
    private readonly AddWeeklyScheduleDto _dto;


    public AddWeeklyScheduleDtoBuilder()
    {
        _dto = new AddWeeklyScheduleDto();

    }

    public AddWeeklyScheduleDtoBuilder WithSchedule()
    {
        _dto.Schedules.Add(new Schedule()
        {
            DayOfWeek = DayWeek.Monday,
            EndTime = DateTime.UtcNow.AddHours(8),
            StartTime = DateTime.UtcNow,
        });
        return this;
    }

    public AddWeeklyScheduleDto Build()
    {
        return _dto;
    }
}
