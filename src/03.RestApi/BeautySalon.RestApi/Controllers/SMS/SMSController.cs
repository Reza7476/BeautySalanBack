using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services;
using BeautySalon.Services.SMSLogs.Contracts;
using BeautySalon.Services.SMSLogs.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.SMS;
[Route("api/sms")]
[ApiController]
public class SMSController : ControllerBase
{
    private readonly ISMSService _smsService;
    private readonly ISMSLogService _smsLogService;

    public SMSController(ISMSService smsService, ISMSLogService smsLogService)
    {
        _smsService = smsService;
        _smsLogService = smsLogService;
    }

    [HttpGet("credit-number-of-sms")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task<GetSMSumberCreditDto?> GetSMSCountCredit()
    {
        return await _smsService.GetSMSCountCredit();
    }

    [HttpGet("all-sent-sms")]
    public async Task<IPageResult<GetAllSentSMSDto>>
        GetAllSentSMS([FromQuery] Pagination? pagination = null,
        [FromQuery] string? search=null)
    {
        return await _smsLogService.GetAllSentSMS(pagination,search);
    }
}
