using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.SMSLogs;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.SMSLogs.Contracts;
using BeautySalon.Services.SMSLogs.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.SMSLogs;
public class EFSMSLogRepository : ISMSLogRepository
{

    private readonly DbSet<SMSLog> _smsLogs;

    public EFSMSLogRepository(EFDataContext context)
    {
        _smsLogs = context.Set<SMSLog>();
    }

    public async Task Add(SMSLog newSMSLog)
    {
        await _smsLogs.AddAsync(newSMSLog);
    }

    public async Task<SMSLog?> FindById(string id)
    {
        return await _smsLogs.FindAsync(id);
    }

    public async Task<IPageResult<GetAllSentSMSDto>>
        GetAllSentSMS(IPagination? pagination = null, string? search = null)
    {
        var query = (from smsLog in _smsLogs
                     select new
                     GetAllSentSMSDto()
                     {
                         CreatedAt = smsLog.CreatedAt,
                         ReceiverNumber = smsLog.ReceiverNumber,
                         RecId = smsLog.RecId,
                         Status = smsLog.Status,
                         Content = smsLog.Content,
                         ResponseContent = smsLog.ResponseContent,
                         Title = smsLog.Title
                     }).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(_ => _.ReceiverNumber!.Contains(search));
        }

        query = query.OrderByDescending(_ => _.CreatedAt);
        return await query.Paginate(pagination ?? new Pagination());
    }
}
