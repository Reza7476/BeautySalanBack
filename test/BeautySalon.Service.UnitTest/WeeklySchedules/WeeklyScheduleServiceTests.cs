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

    [Fact]
    public async Task Edit_should_edit_schedule_properly()
    {
        var schedule = new WeeklyScheduleBuilder()
            .WithIsActive(true)
            .WithDay(DayWeek.Monday)
            .WithStartTime(DateTime.Now)
            .WithEndTime(DateTime.Now.AddHours(8))
            .Build();
        Save(schedule);
        var dto = new EditWeeklyScheduleDtoBuilder()
            .WithStartTime(DateTime.Now)
            .WithEndTime(DateTime.Now.AddHours(8))
            .WithDayOfWeek(DayWeek.Saturday)
            .WithIsActive()
            .WithId(schedule.Id)
            .Build();

        await _sut.EditSchedule(dto);

        var expected = ReadContext.Set<WeeklySchedule>().First();
        expected.DayOfWeek.Should().Be(dto.DayOfWeek);
        expected.StartTime.Should().Be(dto.StartTime);
        expected.EndTime.Should().Be(dto.EndTime);
        expected.IsActive.Should().Be(dto.IsActive);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Edit_should_throw_exception_when_schedule_not_found(int id)
    {
        var dto = new EditWeeklyScheduleDtoBuilder()
            .WithId(id)
            .Build();

        Func<Task> expected = async () => await _sut.EditSchedule(dto);

        await expected.Should().ThrowExactlyAsync<ScheduleNotFoundException>();
    }
}
