using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Services.Clients.Contracts;
public interface IClientService : IService
{
    Task<string> Add(AddClientDto dto);
    
    Task<List<GetAllClientsForAddAppointment>>
        GetAllForAppointment(string? search=null);

    Task<string?> GetClientIdByUserId(string userId);
    Task<bool> IsExistById(string clientId);
}
