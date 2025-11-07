using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Clients;
using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Services.Clients.Contracts.Dtos;
using BeautySalon.Services.Clients.Exceptions;

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
            Id = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.Add(client);
        await _unitOfWork.Complete();
        return client.Id;
    }

    public async Task<List<GetAllClientsForAddAppointment>>
        GetAllForAppointment(string? search=null)
    {
        return await _repository.GetAllForAppointment(search);
    }

    public async Task<IPageResult<GetAllClientAppointmentsDto>>
        GetClientAppointments(
        IPagination? pagination = null,
        ClientAppointmentFilterDto? filter = null,
        string? userId = null)
    {
        if (userId == null)
        {
            throw new YouAreNotAllowedToAccessException();
        }
        var clientId = await _repository.GetClientIdByUserId(userId);
        if (clientId == null)
        {
            throw new YouAreNotAllowedToAccessException();
        }

        return await _repository.GetClientAppointments(clientId, pagination, filter);
    }

    public async Task<string?> GetClientIdByUserId(string userId)
    {
        return await _repository.GetClientIdByUserId(userId);
    }

    public async Task<bool> IsExistById(string id)
    {
        return await _repository.IsExistById(id);
    }
}
