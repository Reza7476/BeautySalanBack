using BeautySalon.Application.Clients.Contracts.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.Application.Clients.Contracts;
public interface ClientHandler : IScope
{
    Task<string> AddNewClient(AddNewClientHandlerDto dto);
}
