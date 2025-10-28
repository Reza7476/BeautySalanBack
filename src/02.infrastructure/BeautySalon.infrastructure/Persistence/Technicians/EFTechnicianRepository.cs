using BeautySalon.Entities.Technicians;
using BeautySalon.Services.Technicians.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Technicians;
public class EFTechnicianRepository : ITechnicianRepository
{
    private readonly DbSet<Technician> _technicians;

    public EFTechnicianRepository(EFDataContext context)
    {
        _technicians = context.Set<Technician>();
    }

    public async Task Add(Technician technician)
    {
        await _technicians.AddAsync(technician);
    }

    public async Task<string?> GetTechnicianId()
    {
        var technician = await _technicians
            .AsNoTracking()
            .FirstOrDefaultAsync();
        return technician?.Id;
    }
}
