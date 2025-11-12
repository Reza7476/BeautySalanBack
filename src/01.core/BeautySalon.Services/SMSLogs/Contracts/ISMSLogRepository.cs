using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.SMSLogs;
using BeautySalon.Services.SMSLogs.Contracts.Dtos;

namespace BeautySalon.Services.SMSLogs.Contracts;
public interface ISMSLogRepository : IRepository
{
    Task Add(SMSLog newSMSLog);
    Task<SMSLog?> FindById(string id);
    Task<IPageResult<GetAllSentSMSDto>> 
        GetAllSentSMS(IPagination? pagination=null, string? search = null);
}
