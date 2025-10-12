using BeautySalon.Application.Treatments.Contracts;
using BeautySalon.Application.Treatments.Contracts.Dto;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Application.Treatments;
public class TreatmentCommandHandler : TreatmentHandler
{
    private readonly ITreatmentService _service;
    private readonly IMediaService _mediaService;

    public TreatmentCommandHandler(
        ITreatmentService service,
        IMediaService mediaService)
    {
        _service = service;
        _mediaService = mediaService;
    }

    public async Task<long> Add(AddTreatmentHandlerDto dto)
    {
        var media = await _mediaService.SaveMedia(new AddMediaDto()
        {
            Media = dto.Media,
        });

        var treatmentId = await _service.Add(new AddTreatmentDto()
        {
            Description = dto.Description,
            Title = dto.Title,
            ImageName = media.ImageName,
            ImageUniqueName = media.UniqueName,
            URL = media.URL,
            Extension = media.Extension
        });

        return treatmentId;
    }

    public async Task<long> AddImage(long id, AddMediaDto dto)
    {
        var media = await _mediaService.SaveMedia(new AddMediaDto()
        {
            Media = dto.Media,
        });

        var treatmentImageId = await _service.AddImageReturnImageId(id, new ImageDetailsDto
        {
            Extension = media.Extension,
            ImageName = media.ImageName,
            UniqueName = media.UniqueName,
            URL = media.URL,
        });

        return treatmentImageId;
    }

    public async Task DeleteImage(long imageId, long id)
    {
        var url = await _service.GetUrl_Remove_Image(imageId, id);
        await _mediaService.DeleteMediaByURL(url);
    }
}
