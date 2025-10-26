using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;

namespace BeautySalon.Services.WeeklySchedules.Contracts;
public interface IWeeklyScheduleRepository : IRepository
{
    Task Add(WeeklySchedule schedules);
    Task<List<GetScheduleDto>> GetSchedules();
    Task<bool> IsExistByDayOfWeek(DayWeek dayOfWeek);
}
