using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts;
using BeautySalon.Services.WeeklySchedules.Exceptions;
using BeautySalon.Test.Tool.Entities.WeeklySchedules;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.WhyUSSections;
public class WeeklyScheduleServiceTests : BusinessUnitTest
{

    private readonly IWeeklyScheduleService _sut;

    public WeeklyScheduleServiceTests()
    {
        _sut = WeeklyScheduleServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Add_should_add_weekly_schedules_properly()
    {
        var dto = new AddWeeklyScheduleDtoBuilder()
            .WithDayOfWeek(DayWeek.Saturday)
            .WithEndTime(DateTime.Now.AddHours(1))
            .WithStartTime(DateTime.Now)
            .WithIsActive()
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<WeeklySchedule>().First();
        expected.StartTime.Should().Be(dto.StartTime);
        expected.EndTime.Should().Be(dto.EndTime);
        expected.DayOfWeek.Should().Be(dto.DayOfWeek);
        expected.IsActive.Should().Be(dto.IsActive);
    }

    [Fact]
    public async Task Add_should_throw_exception_when_day_of_week_is_existed()
    {
        var schedule = new WeeklyScheduleBuilder()
            .WithDay(DayWeek.Saturday)
            .Build();
        Save(schedule);
        var dto = new AddWeeklyScheduleDtoBuilder()
            .WithDayOfWeek(DayWeek.Saturday)
            .Build();
        Func<Task> expected = async () => await _sut.Add(dto);

        await expected.Should().ThrowAsync<DayOfWeekIsDuplicateException>();
    }
}
