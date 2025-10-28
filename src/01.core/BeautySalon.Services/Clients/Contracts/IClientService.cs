using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Services.Clients.Contracts;
public interface IClientService : IService
{
    Task<string> Add(AddClientDto dto);
}
