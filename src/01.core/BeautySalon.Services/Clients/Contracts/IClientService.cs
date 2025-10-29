using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Services.Clients.Contracts;
public interface IClientService : IService
{
    Task<string> Add(AddClientDto dto);
    
    Task<IPageResult<GetAllClientAppointmentsDto>> GetClientAppointments(
        IPagination? pagination=null, 
        ClientAppointmentFilterDto? filter = null,
        string? userId=null);
   
    Task<string?> GetClientIdByUserId(string userId);
}
