using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Clients.Contracts;

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
}
