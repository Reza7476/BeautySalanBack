﻿using BeautySalon.Common.Interfaces;
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
            CreatedAt=DateTime.UtcNow,
            ErrorMessage=dto.ErrorMessage,
            Id=Guid.NewGuid().ToString(),
            Message=dto.Message,
            ProviderNumber= dto.ProviderNumber,
            ReceiverNumber=dto.ReceiverNumber,
            Status= SendSMSStatus.Pending,
            RecId=dto.RecId
        };
        
        await _repository.Add(newSMSLog);
        await _unitOfWork.Complete();
        return newSMSLog.Id;
    
    }
}
