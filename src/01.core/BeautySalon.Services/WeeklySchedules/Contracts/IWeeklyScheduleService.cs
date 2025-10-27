using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;

namespace BeautySalon.Services.WeeklySchedules.Contracts;
public interface IWeeklyScheduleService : IService
{
    Task<int> Add(AddWeeklyScheduleDto dto);
    Task EditSchedule(EditWeeklyScheduleDto dto);
    Task<GetDayScheduleDto?> GetDaySchedule(DayWeek dayWeek);
    Task<List<GetScheduleDto>> GetSchedules();
}
