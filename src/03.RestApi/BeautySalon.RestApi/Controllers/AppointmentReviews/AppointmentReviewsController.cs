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

}
