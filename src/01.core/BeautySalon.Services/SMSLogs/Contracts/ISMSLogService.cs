using BeautySalon.Common.Interfaces;
using BeautySalon.Services.SMSLogs.Contracts.Dtos;

namespace BeautySalon.Services.SMSLogs.Contracts;
public interface ISMSLogService : IService
{
    Task<string> Add(AddSMSLogDto dto);
}
