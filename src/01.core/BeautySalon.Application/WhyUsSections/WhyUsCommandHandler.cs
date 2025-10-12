using BeautySalon.Application.WhyUsSections.Contracts;
using BeautySalon.Application.WhyUsSections.Contracts.Dto;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Common.Interfaces;
using BeautySalon.Common.Dtos;
using BeautySalon.Services.WhyUsSections.Exceptions;

namespace BeautySalon.Application.WhyUsSections;
public class WhyUsCommandHandler : IWhyUsSectionHandler
{
    private readonly IMediaService _mediaService;
    private readonly IWhyUsSectionService _service;

    public WhyUsCommandHandler(
        IMediaService mediaService,
        IWhyUsSectionService service)
    {
        _mediaService = mediaService;
        _service = service;
    }

    public async Task<long> Add(AddWhyUsSectionHandlerDto dto)
    {
        var media = await _mediaService.SaveMedia(new AddMediaDto()
        {
            Media = dto.Image
        });

        var whyUsSectionId = await _service.Add(new AddWhyUsSectionDto()
        {
            Title = dto.Title,
            Media = new MediaDto()
            {
                Extension = media.Extension,
                URL = media.URL,
                ImageName = media.ImageName,
                UniqueName = media.UniqueName
            },
            Description= dto.Description,
        });

        return whyUsSectionId;
    }

    public async Task UpdateImage(long id, AddMediaDto dto)
    {
        var imgUrl = await _service.GetById(id);
        if (imgUrl == null)
        {
            throw new WhyUsSectionNotFoundException();
        }
        await _mediaService.DeleteMediaByURL(imgUrl.Image.URL);
        MediaDto media=await _mediaService.SaveMedia(new AddMediaDto()
        {
            Media=dto.Media
        });

        await _service.UpdateImage(id, new ImageDetailsDto()
        {
            Extension = media.Extension,
            ImageName = media.ImageName,
            UniqueName = media.UniqueName,
            URL = media.URL
        });


    }
}
