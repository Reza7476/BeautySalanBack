using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Clients;
using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Services.Clients.Contracts;
public interface IClientRepository : IRepository
{
    Task Add(Client client);

    Task<List<GetAllClientsForAddAppointment>> GetAllForAppointment(string? search=null);
    
    Task<IPageResult<GetAllClientAppointmentsDto>> GetClientAppointments(
        string id,
        IPagination? pagination = null,
        ClientAppointmentFilterDto? filter = null);

    Task<string?> GetClientIdByUserId(string userId);
}
