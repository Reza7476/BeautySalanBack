using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.SMSLogs;
using BeautySalon.Services.SMSLogs.Contracts.Dtos;

namespace BeautySalon.Services.SMSLogs.Contracts;
public interface ISMSLogService : IService
{
    Task<string> Add(AddSMSLogDto dto);
    Task ChangeStatus(string id, SendSMSStatus status);
    Task<IPageResult<GetAllSentSMSDto>>
        GetAllSentSMS(IPagination? pagination=null, string? search = null);
}
