using BeautySalon.Application.Treatments.Contracts.Dto;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.Application.Treatments.Contracts;
public interface TreatmentHandler : IScope
{
    Task<long> Add(AddTreatmentHandlerDto dto);
    Task<long> AddImage(long id, AddMediaDto dto);
    Task DeleteImage(long imageId, long id);
}
