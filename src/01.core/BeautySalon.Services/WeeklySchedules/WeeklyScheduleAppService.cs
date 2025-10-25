using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;

namespace BeautySalon.Services.WeeklySchedules;
public class WeeklyScheduleAppService : IWeeklyScheduleService
{
    private readonly IWeeklyScheduleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WeeklyScheduleAppService(
        IWeeklyScheduleRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddWeeklyScheduleDto dto)
    {

        var schedules = new List<WeeklySchedule>();

        foreach (var item in dto.Schedules)
        {
            var schudule = new WeeklySchedule()
            {
                DayOfWeek = item.DayOfWeek,
                EndTime = item.EndTime,
                IsActive = true,
                StartTime = item.StartTime,
            };
            schedules.Add(schudule);
        }

        await _repository.AddRange(schedules);
        await _unitOfWork.Complete();
    }
}
