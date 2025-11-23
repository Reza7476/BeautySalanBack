using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Treatments;
using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Services.Treatments.Contracts;
public interface ITreatmentRepository : IRepository
{
    Task Add(Treatment treatment);
    Task AddImage(TreatmentImage treatmentImage);
    Task<bool> ExistById(long id);
    Task<Treatment?> FindById(long id);
    Task<TreatmentImage?> FindImageByImageId(long imageId);
    Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination);
    Task<List<GetAllTreatmentsForAppointmentDto>> GetAllForAppointment();
    Task<List<GetTreatmentTitleForListAppointmentFilterDto>> GetAllTitles();
    Task<GetTreatmentDetailsDto?> GetDetails(long id);
    Task<GetTreatmentDetailsForAppointmentDto?> GetDetailsForAppointment(long id);
    Task<List<GetTreatmentForLandingDto>> GetForLanding();
    Task<List<GetPopularTreatmentsDto>> GetPopularTreatments();
    Task <List<TreatmentImage>>GetTreatmentImages(long id);
    
    Task<IPageResult<GetAllTreatmentGalleryImageDto>> 
        GetTreatmentsGalleryForLanding(IPagination? pagination);
    
    Task<bool> IsExistByTitle(string title);
    Task RemoveImage(TreatmentImage treatmentImage);
}
