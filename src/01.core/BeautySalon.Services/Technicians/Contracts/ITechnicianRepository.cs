using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Technicians;

namespace BeautySalon.Services.Technicians.Contracts;
public interface ITechnicianRepository : IRepository
{
    Task Add(Technician technician);
    Task<string?> GetTechnicianId();
}
