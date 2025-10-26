using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts;
using BeautySalon.Test.Tool.Entities.WeeklySchedules;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.WeeklySchedules;
public class WeeklyScheduleServiceTests : BusinessIntegrationTest
{
    private readonly IWeeklyScheduleService _sut;

    public WeeklyScheduleServiceTests()
    {
        _sut = WeeklyScheduleServiceFactory.Generate(SetupContext);
    }

    [Fact]
    public async Task GetSchedule_should_return_schedule_properly()
    {
        var schedules = new WeeklyScheduleBuilder()
            .WithIsActive(true)
            .WithDay(DayWeek.Monday)
            .WithEndTime(DateTime.UtcNow.AddHours(8))
            .WithStartTime(DateTime.UtcNow)
            .Build();
        Save(schedules);

        var expected = await _sut.GetSchedules();

        expected.First().DayOfWeek.Should().Be(schedules.DayOfWeek);
        expected.First().IsActive.Should().Be(schedules.IsActive);
        expected.First().StartTime.Should().Be(schedules.StartTime);
        expected.First().EndTime.Should().Be(schedules.EndTime);
    }

}
