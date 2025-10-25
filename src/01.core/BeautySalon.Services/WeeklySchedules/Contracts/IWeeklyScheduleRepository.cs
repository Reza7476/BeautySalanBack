using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Services.WeeklySchedules.Contracts;
public interface IWeeklyScheduleRepository : IRepository
{
    Task AddRange(List<WeeklySchedule> schedules);
}
