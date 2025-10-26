using BeautySalon.Services.WeeklySchedules.Contracts;
using BeautySalon.Services.WeeklySchedules.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<int> Add([FromBody]AddWeeklyScheduleDto dto)
    {
       return  await _service.Add(dto);
    }

    [HttpGet]
    public async Task<List<GetScheduleDto>> GetSchedule()
    {
        return await _service.GetSchedules();
    }

    [Authorize]
    [HttpPut]
    public async Task Edit( [FromBody] EditWeeklyScheduleDto dto)
    {
        await _service.EditSchedule(dto);
    }
}
