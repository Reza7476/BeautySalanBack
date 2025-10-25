using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts;
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
}
