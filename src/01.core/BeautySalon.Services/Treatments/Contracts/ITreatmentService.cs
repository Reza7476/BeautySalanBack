using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Services.Treatments.Contracts;
public interface ITreatmentService : IService
{
    Task<long> Add(AddTreatmentDto dto);
    Task<long> AddImageReturnImageId(long id, ImageDetailsDto dto);
    Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination = null);
    Task<List<GetAllTreatmentsForAppointmentDto>> GetAllForAppointment();
    Task<GetTreatmentDetailsDto?> GetDetails(long id);
    Task<GetTreatmentDetailsForAppointmentDto?> GetDetailsForAppointment(long id);
    Task<List<GetTreatmentForLandingDto>> GetForLanding();
    Task<string> GetUrl_Remove_Image(long imageId, long id);
    Task Update(UpdateTreatmentDto dto, long id);
}
