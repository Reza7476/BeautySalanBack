using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.SMSLogs;

namespace BeautySalon.Services.SMSLogs.Contracts;
public interface ISMSLogRepository : IRepository
{
    Task Add(SMSLog newSMSLog);
}
