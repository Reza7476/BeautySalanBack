using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules.Contracts;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
using BeautySalon.Services.WeeklySchedules.Exceptions;

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

    public async Task<int> Add(AddWeeklyScheduleDto dto)
    {
        if(await _repository.IsExistByDayOfWeek(dto.DayOfWeek))
        {
            throw new DayOfWeekIsDuplicateException();
        }

        var schedule = new WeeklySchedule()
        {
            DayOfWeek = dto.DayOfWeek,
            EndTime= dto.EndTime,
            IsActive=dto.IsActive,
            StartTime= dto.StartTime    
        };
        

        await _repository.Add(schedule);
        await _unitOfWork.Complete();
        return schedule.Id;
    }

    public async Task<List<GetScheduleDto>> GetSchedules()
    {
        return await _repository.GetSchedules();
    }
}
