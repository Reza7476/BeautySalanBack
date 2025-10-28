using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Technicians.Contracts.Dtos;

namespace BeautySalon.Services.Technicians.Contracts;
public interface ITechnicianService : IService
{
    Task<string> Add(AddTechnicianDto dto);
    Task<string?> GetTechnicianId();
}
