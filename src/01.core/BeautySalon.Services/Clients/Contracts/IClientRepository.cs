using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Clients;
using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Services.Clients.Contracts;
public interface IClientRepository : IRepository
{
    Task Add(Client client);

    Task<List<GetAllClientsForAddAppointment>> 
        GetAllForAppointment(string? search=null);

    Task<string?> GetClientIdByUserId(string userId);
    Task<bool> IsExistById(string id);
}
