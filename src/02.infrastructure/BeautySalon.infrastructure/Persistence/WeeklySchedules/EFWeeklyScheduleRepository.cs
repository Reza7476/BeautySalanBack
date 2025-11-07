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

    public async Task Add(WeeklySchedule schedules)
    {
        await _weeklySchedules.AddAsync(schedules);
    }

    public async Task<WeeklySchedule?> FindById(int id)
    {
        return await _weeklySchedules.FindAsync(id);
    }

    public async Task<GetDayScheduleDto?> GetDaySchedule(DayWeek dayWeek)
    {
        return await _weeklySchedules
            .Where(_ => _.DayOfWeek == dayWeek)
            .Select(_ => new GetDayScheduleDto()
            {
                StartTime = _.StartTime,
                EndTime = _.EndTime,
                IsActive=_.IsActive
            }).FirstOrDefaultAsync();
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

    public async Task<bool> IsExistByDayOfWeek(DayWeek dayOfWeek)
    {
        return await _weeklySchedules.AnyAsync(_ => _.DayOfWeek == dayOfWeek);
    }
}
