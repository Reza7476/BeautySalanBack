using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.WeeklySchedules;
public class EFWeeklyScheduleRepository : IWeeklyScheduleRepository
{
    private readonly DbSet<WeeklySchedule> _weeklySchedules;


    public EFWeeklyScheduleRepository(EFDataContext context)
    {
        _weeklySchedules = context.Set<WeeklySchedule>();
    }

    public async Task AddRange(List<WeeklySchedule> schedules)
    {
        await _weeklySchedules.AddRangeAsync(schedules);
    }

    public async Task<List<GetScheduleDto>> GetSchedules()
    {
        var aa = await _weeklySchedules.Select(_ => new GetScheduleDto()
        {
            DayOfWeek = _.DayOfWeek,
            EndTime = _.EndTime,
            Id = _.Id,
            IsActive = _.IsActive,
            StartTime = _.StartTime,

        }).ToListAsync();
        return aa;
    }
}
