using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Treatments;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Treatments;
public class EFTreatmentRepository : ITreatmentRepository
{
    private readonly DbSet<Treatment> _treatments;
    private readonly DbSet<TreatmentImage> _treatmentImages;

    public EFTreatmentRepository(EFDataContext context)
    {
        _treatments = context.Set<Treatment>();
        _treatmentImages = context.Set<TreatmentImage>();

    }

    public async Task Add(Treatment treatment)
    {
        await _treatments.AddAsync(treatment);
    }

    public async Task AddImage(TreatmentImage treatmentImage)
    {
        await _treatmentImages.AddAsync(treatmentImage);
    }

    public async Task<bool> ExistById(long id)
    {
        return await _treatments.AnyAsync(_ => _.Id == id);
    }

    public async Task<Treatment?> FindById(long id)
    {
        return await _treatments.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<TreatmentImage?> FindImageByImageId(long imageId)
    {
        return await _treatmentImages.FirstOrDefaultAsync(_ => _.Id == imageId);
    }

    public async Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination)
    {
        var query = _treatments
            .Include(_ => _.Images)
            .Select(_ => new GetAllTreatmentsDto()
            {
                Description = _.Description,
                Title = _.Title,
                Id = _.Id,
                Media = _.Images.Select(media => new MediaDto()
                {
                    Extension = media.Extension,
                    URL = media.URL,
                    ImageName = media.ImageName,
                    UniqueName = media.ImageUniqueName
                }).First()
            }).AsQueryable();

        return await query.Paginate(pagination ?? new Pagination());
    }


    public async Task<GetTreatmentDetailsDto?> GetDetails(long id)
    {
        return await _treatments
            .Where(_ => _.Id == id)
            .Include(_ => _.Images)
            .Select(_ => new GetTreatmentDetailsDto()
            {
                Description = _.Description,
                Title = _.Title,
                Media = _.Images.Select(media => new MediaDto()
                {
                    Extension = media.Extension,
                    URL = media.URL,
                    UniqueName = media.ImageUniqueName,
                    ImageName = media.ImageName,
                    Id = media.Id
                }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<List<GetTreatmentForLandingDto>> GetForLanding()
    {
        return await _treatments
            .Include(_ => _.Images)
            .Take(10)
            .Select(_ => new GetTreatmentForLandingDto()
            {
                Description = _.Description,
                Id = _.Id,
                Title = _.Title,
                Media = _.Images != null ? _.Images.Select(media => new MediaDto()
                {
                    Extension = media.Extension,
                    ImageName = media.ImageName,
                    UniqueName = media.ImageUniqueName,
                    URL = media.URL,
                    Id=media.Id
                }).FirstOrDefault() : null
            }).ToListAsync();
    }

    public async Task<List<TreatmentImage>> GetTreatmentImages(long id)
    {
        return await _treatmentImages.Where(_ => _.TreatmentId == id).ToListAsync();
    }

    public async Task RemoveImage(TreatmentImage treatmentImage)
    {
        _treatmentImages.Remove(treatmentImage);
        await Task.CompletedTask;
    }
}
