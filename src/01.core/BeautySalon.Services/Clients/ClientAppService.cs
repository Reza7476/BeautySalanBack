using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Clients;
using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Services.Clients;
public class ClientAppService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ClientAppService(
        IClientRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Add(AddClientDto dto)
    {
        var client = new Client()
        {
            Id=Guid.NewGuid().ToString(),
            UserId = dto.UserId,
        };

        await _repository.Add(client);
        await _unitOfWork.Complete();
        return client.Id;
    }

    public async Task<string?> GetClientIdByUserId(string userId)
    {
        return await _repository.GetClientIdByUserId(userId);
    }
}
