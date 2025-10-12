using BeautySalon.Application.WhyUsSections.Contracts;
using BeautySalon.Application.WhyUsSections.Contracts.Dto;
using BeautySalon.Common.Dtos;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.WhyUsSections;
[Route("api/why-us-sections")]
[ApiController]
public class WhyUsSectionsController : ControllerBase
{
    private readonly IWhyUsSectionHandler _handler;
    private readonly IWhyUsSectionService _service;


    public WhyUsSectionsController(
        IWhyUsSectionHandler handler,
        IWhyUsSectionService service)
    {
        _handler = handler;
        _service = service;
    }

    [HttpPost("add")]
    public async Task<long> Add([FromForm] AddWhyUsSectionHandlerDto dto)
    {
        return await _handler.Add(dto);
    }

    [HttpPost("{sectionId}/add-question")]
    public async Task<long> AddQuestion(
        [FromRoute] long sectionId,
        [FromBody] AddWhyUsQuestionDto dto)
    {
        return await _service.AddQuestion(dto, sectionId);
    }

    [HttpGet("{sectionId}/questions")]
    public async Task<List<GetWhyUsQuestionsDto>>
        GetQuestions([FromRoute] long sectionId)
    {
        return await _service.GetQuestionsBySectionId(sectionId);
    }

    [HttpGet]
    public async Task<GetWhyUsSectionDto?> GetWhyUsSection()
    {
        return await _service.GetWhyUsSection();
    }

    [HttpPut("{questionId}/question")]
    public async Task UpdateQuestion(
        [FromRoute] long questionId,
        [FromBody] UpdateWhyUsQuestionDto dto)
    {
        await _service.UpdateQuestion(questionId, dto);
    }

    [HttpPut("{id}")]
    public async Task UpdateWhyUsSection(
        [FromRoute] long id,
        [FromBody] EditTitleAndDescriptionWhyUsSectionDto dto)
    {
        await _service.UpdateWhyUsSection(id, dto);
    }

    [HttpDelete("{questionId}/question")]
    public async Task Delete([FromRoute] long questionId)
    {
        await _service.DeleteQuestion(questionId);
    }

    [HttpGet("{id}")]
    public async Task<GetWhyUsSectionForEditDto?> GetWhyUsSectionForEdit([FromRoute] long id)
    {
        return await _service.GetWhyUsSectionByIdForEdit(id);
    }

    [HttpPatch("{id}/image")]
    public async Task UpdateImage(
        [FromRoute] long id,
        [FromForm] AddMediaDto dto)
    {
        await _handler.UpdateImage(id, dto);
    }

    [HttpGet("all-for-landing")]
    public async Task<GetWhyUsForLandingDto?> GetForLanding()
    {
        return await _service.GetForLanding();
    }
}
