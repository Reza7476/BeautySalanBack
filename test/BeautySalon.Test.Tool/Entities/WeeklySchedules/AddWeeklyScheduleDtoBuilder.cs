using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.WeeklySchedules;
public class AddWeeklyScheduleDtoBuilder
{
    private readonly AddWeeklyScheduleDto _dto;


    public AddWeeklyScheduleDtoBuilder()
    {
        _dto = new AddWeeklyScheduleDto()
        {
            DayOfWeek = DayWeek.Saturday,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(8),
            IsActive = false
        };
    }

    public AddWeeklyScheduleDtoBuilder WithStartTime(DateTime time)
    {
        _dto.StartTime = time;
        return this;
    }

    public AddWeeklyScheduleDtoBuilder WithEndTime(DateTime time)
    {
        _dto.EndTime = time;
        return this;
    }


    public AddWeeklyScheduleDtoBuilder WithIsActive()
    {
        _dto.IsActive = true;
        return this;
    }


    public AddWeeklyScheduleDtoBuilder WithDayOfWeek(DayWeek day)
    {
        _dto.DayOfWeek = day;
        return this;
    }

    public AddWeeklyScheduleDto Build()
    {
        return _dto;
    }
}
