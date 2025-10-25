using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;

namespace BeautySalon.Services.WeeklySchedules.Contracts;
public interface IWeeklyScheduleRepository : IRepository
{
    Task AddRange(List<WeeklySchedule> schedules);
    Task<List<GetScheduleDto>> GetSchedules();
}
