using BeautySalon.Application.Treatments.Contracts;
using BeautySalon.Application.Treatments.Contracts.Dto;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.Treatments;
[Route("api/treatments")]
[ApiController]
public class TreatmentsController : ControllerBase
{
    private readonly TreatmentHandler _handler;
    private readonly ITreatmentService _service;


    public TreatmentsController(
        TreatmentHandler handler,
        ITreatmentService service)
    {
        _handler = handler;
        _service = service;
    }

    [HttpPost("add")]
    public async Task<long> Add([FromForm] AddTreatmentHandlerDto dto)
    {
        return await _handler.Add(dto);
    }

    [HttpGet("all")]
    public async Task<IPageResult<GetAllTreatmentsDto>> GetAll([FromQuery] Pagination? pagination = null)
    {
        return await _service.GetAll(pagination);
    }


    [HttpGet("{id}")]
    public async Task<GetTreatmentDetailsDto?> GetDetails(long id)
    {
        return await _service.GetDetails(id);
    }

    [HttpPost("{id}/add-image")]
    public async Task<long> AddImage([FromRoute] long id, [FromForm] AddMediaDto dto)
    {
        return await _handler.AddImage(id, dto);
    }

    [HttpDelete("{id}/{imageId}/image")]
    public async Task DeleteImage(long imageId, long id)
    {
        await _handler.DeleteImage(imageId, id);
    }


    [HttpPut("{id}")]
    public async Task Update([FromRoute] long id, [FromBody]UpdateTreatmentDto dto)
    {
        await _service.Update(dto,id);
    }


    [HttpGet("for-landing")]
    public async Task<List<GetTreatmentForLandingDto>> GetForLanding()
    {
        return await _service.GetForLanding();
    }
}
