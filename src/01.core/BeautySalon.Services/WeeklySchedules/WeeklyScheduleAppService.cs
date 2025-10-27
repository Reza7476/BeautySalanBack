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
        if (await _repository.IsExistByDayOfWeek(dto.DayOfWeek))
        {
            throw new DayOfWeekIsDuplicateException();
        }

        if (dto.EndTime < dto.StartTime)
        {
            throw new EndTimeIsLessThanStartTimeException();
        }

        var schedule = new WeeklySchedule()
        {
            DayOfWeek = dto.DayOfWeek,
            EndTime = dto.EndTime,
            IsActive = dto.IsActive,
            StartTime = dto.StartTime
        };

        await _repository.Add(schedule);
        await _unitOfWork.Complete();
        return schedule.Id;
    }

    public async Task EditSchedule(EditWeeklyScheduleDto dto)
    {
        var schedule = await _repository.FindById(dto.Id);

        if (schedule == null)
        {
            throw new ScheduleNotFoundException();
        }


        if (dto.EndTime < dto.StartTime)
        {
            throw new EndTimeIsLessThanStartTimeException();
        }

        schedule.StartTime = dto.StartTime;
        schedule.EndTime = dto.EndTime;
        schedule.IsActive = dto.IsActive;
        schedule.DayOfWeek = dto.DayOfWeek;
        await _unitOfWork.Complete();
    }

    public async Task<GetDayScheduleDto?> GetDaySchedule(DayWeek dayWeek)
    {
        return await _repository.GetDaySchedule(dayWeek);
    }

    public async Task<List<GetScheduleDto>> GetSchedules()
    {
        return await _repository.GetSchedules();
    }
}
