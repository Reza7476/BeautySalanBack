using BeautySalon.Application.OtpRequests.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.OtpRequests;
[Route("api/otp-requests")]
[ApiController]
public class OtpRequestsController : ControllerBase
{
    private readonly IOtpRequestHandle _handler;

    public OtpRequestsController(IOtpRequestHandle handler)
    {
        _handler = handler;
    }

}
