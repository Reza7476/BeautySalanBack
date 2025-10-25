using BeautySalon.Common.Interfaces;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;

namespace BeautySalon.Services.WeeklySchedules.Contracts;
public interface IWeeklyScheduleService : IService
{
    Task Add(AddWeeklyScheduleDto dto);
    Task<List<GetScheduleDto>> GetSchedules();
}
