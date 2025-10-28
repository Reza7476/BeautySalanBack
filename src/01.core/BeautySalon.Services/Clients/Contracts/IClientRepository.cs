using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Clients;

namespace BeautySalon.Services.Clients.Contracts;
public interface IClientRepository : IRepository
{
    Task Add(Client client);
}
