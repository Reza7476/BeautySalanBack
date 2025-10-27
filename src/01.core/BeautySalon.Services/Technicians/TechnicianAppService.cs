using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Technicians;
using BeautySalon.Services.Technicians.Contracts;
using BeautySalon.Services.Technicians.Contracts.Dtos;

namespace BeautySalon.Services.Technicians;
public class TechnicianAppService : ITechnicianService
{
    private readonly ITechnicianRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TechnicianAppService(
        ITechnicianRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Add(AddTechnicianDto dto)
    {
        var technician = new Technician()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            CreatedDate = DateTime.UtcNow,
        };
        await _repository.Add(technician);
        await _unitOfWork.Complete();

        return technician.Id;
    }
}
