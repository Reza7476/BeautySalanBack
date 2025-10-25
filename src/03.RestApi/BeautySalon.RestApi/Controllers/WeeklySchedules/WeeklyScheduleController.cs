using BeautySalon.Services.WeeklySchedules.Contracts;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.WeeklySchedules;
[Route("api/weekly-schedules")]
[ApiController]
public class WeeklyScheduleController : ControllerBase
{
    private readonly IWeeklyScheduleService _service;

    public WeeklyScheduleController(IWeeklyScheduleService service)
    {
        _service = service;
    }


    [HttpPost]

    public async Task Add(AddWeeklyScheduleDto dto)
    {
        await _service.Add(dto);
    }

    [HttpGet]
    public async Task<List<GetScheduleDto>> GetSchedule()
    {
        return await _service.GetSchedules();
    }
}
