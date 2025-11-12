using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.SMSLogs;
using BeautySalon.Services.SMSLogs.Contracts;
using BeautySalon.Services.SMSLogs.Contracts.Dtos;

namespace BeautySalon.Services.SMSLogs;
public class SMSLogAppService : ISMSLogService
{

    private readonly ISMSLogRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SMSLogAppService(
        ISMSLogRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Add(AddSMSLogDto dto)
    {
        var newSMSLog = new SMSLog()
        {
            CreatedAt = DateTime.UtcNow,
            ResponseContent = dto.ResponseContent,
            Id = Guid.NewGuid().ToString(),
            Content = dto.Content,
            ReceiverNumber = dto.ReceiverNumber,
            Status = dto.Status,
            RecId = dto.RecId,
            Title=dto.Title
        };

        await _repository.Add(newSMSLog);
        await _unitOfWork.Complete();
        return newSMSLog.Id;

    }

    public async Task ChangeStatus(string id, SendSMSStatus status)
    {
        var smsLog = await _repository.FindById(id);
        if (smsLog != null)
        {
            smsLog.Status = status;
            await _unitOfWork.Complete();
        }

    }

    public async Task<IPageResult<GetAllSentSMSDto>>
        GetAllSentSMS(IPagination? pagination = null, string? search = null)
    {
        return await _repository.GetAllSentSMS(pagination,search);
    }
}
