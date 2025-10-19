using BeautySalon.Entities.SMSLogs;
using BeautySalon.Services.SMSLogs.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.SMSLogs;
public class EFSMSLogRepository : ISMSLogRepository
{

    private readonly DbSet<SMSLog> _smsLogs;

    public EFSMSLogRepository(EFDataContext context)
    {
        _smsLogs=context.Set<SMSLog>(); 
    }

    public async Task Add(SMSLog newSMSLog)
    {
        await _smsLogs.AddAsync(newSMSLog);
    }

    public async Task<SMSLog?> FindById(string id)
    {
        return await _smsLogs.FindAsync(id);
    }
}
