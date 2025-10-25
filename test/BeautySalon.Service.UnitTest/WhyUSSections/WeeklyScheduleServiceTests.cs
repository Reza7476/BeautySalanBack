using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts;
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
            .WithSchedule()
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<WeeklySchedule>().First();
        expected.StartTime.Should().Be(dto.Schedules.First().StartTime);
        expected.EndTime.Should().Be(dto.Schedules.First().EndTime);
        expected.DayOfWeek.Should().Be(dto.Schedules.First().DayOfWeek);
    }
}
