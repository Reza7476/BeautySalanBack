using BeautySalon.Application.ContactUs.Contacts;
using BeautySalon.Application.ContactUs.Contacts.Dtos;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.ContactUs.Contracts;
using BeautySalon.Services.ContactUs.Contracts.Dto;
using BeautySalon.Services.ContactUs.Exceptions;
using System.Data.Common;

namespace BeautySalon.Application.ContactUs;
public class AboutUsCommandHandler : ContactUsHandler
{

    private readonly IAboutUsService _service;
    private readonly IMediaService _mediaService;

    public AboutUsCommandHandler(
        IAboutUsService service,
        IMediaService mediaService)
    {
        _service = service;
        _mediaService = mediaService;
    }

    public async Task<long> Add(AddAboutUsHandlerDto dto)
    {
        MediaDto? media = null;
        if (dto.LogoImage != null)
        {
            media = await _mediaService.SaveMedia(new AddMediaDto
            {
                Media = dto.LogoImage
            });
        }
        var aboutUs = await _service.Add(new AddAboutUsDto()
        {
            Description = dto.Description,
            Address = dto.Address,
            MobileNumber = dto.MobileNumber,
            Email = dto.Email,
            Instagram = dto.Instagram,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Telephone = dto.Telephone,
            LogoDetails = media != null ? new ImageDetailsDto()
            {
                Extension = media.Extension,
                ImageName = media.ImageName,
                UniqueName = media.UniqueName,
                URL = media.URL
            } : null
        });

        return aboutUs;
    }

    public async Task EditLogo(long id, EditLogoDto dto)
    {

        var logoImage = await _service.GetById(id);
        if (logoImage == null)
        {
            throw new AboutUsNotFoundException();
        }
        if (logoImage.LogoImage != null && logoImage.LogoImage.URL != "")
        {
            await _mediaService.DeleteMediaByURL(logoImage.LogoImage.URL);
        }

        MediaDto media = await _mediaService.SaveMedia(new AddMediaDto()
        {
            Media = dto.Media,

        });

        await _service.UpdateLogo(id, new ImageDetailsDto()
        {
            Extension = media.Extension,
            ImageName = media.ImageName,
            UniqueName = media.UniqueName,
            URL = media.URL
        });
    }
}
