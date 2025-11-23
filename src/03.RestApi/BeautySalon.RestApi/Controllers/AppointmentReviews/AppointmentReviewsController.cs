using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services;
using BeautySalon.Services.AppointmentReviews.Contracts;
using BeautySalon.Services.AppointmentReviews.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.AppointmentReviews;
[Route("api/appointment-reviews")]
[ApiController]
public class AppointmentReviewsController : ControllerBase
{
    private readonly IAppointmentReviewService _service;

    public AppointmentReviewsController(IAppointmentReviewService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles =SystemRole.Client)]
    public async Task<string> AddReview([FromBody] AddAppointmentReviewDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpGet("all-for-admin")]
    [Authorize(Roles =SystemRole.Admin)]
    public async Task<IPageResult<GetAllReviewsDto>> 
        GetAllComments([FromQuery] Pagination? pagination=null)
    {
        return await _service.GetAllCommentsForAdmin(pagination);
    }
}
