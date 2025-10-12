using BeautySalon.Application.Banners.Contracts;
using BeautySalon.Application.Banners.Contracts.Dtos;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Banners;
using BeautySalon.Services.Banners.Contracts;
using BeautySalon.Services.Banners.Contracts.Dto;
using BeautySalon.Services.Banners.Exceptions;

namespace BeautySalon.Application.Banners;
public class BannerCommandHandler : BannerHandler
{
    private readonly IBannerService _bannerService;
    private readonly IMediaService _imageService;

    public BannerCommandHandler(
        IBannerService bannerService,
        IMediaService imageService)
    {
        _bannerService = bannerService;
        _imageService = imageService;
    }

    public async Task<long> Add(AddBannerHandlerDto dto)
    {
        MediaDto media = await _imageService.SaveMedia(new AddMediaDto
        {
            Media = dto.Image
        });

        var bannerId = await _bannerService.Add(new AddBannerDto
        {
            Title = dto.Title,
            Extension = media.Extension,
            ImageName = media.ImageName,
            UniqueName = media.UniqueName,
            URL = media.URL
        });

        return bannerId;
    }

    public async Task UpdateBanner(long id, UpdateBannerHandlerDto dto)
    {
        var banner = await _bannerService.GetById(id);
        StopIfBannerNotFound(banner);

        try
        {
            await _imageService.DeleteMediaByURL(banner!.URL);
            MediaDto media = await _imageService.SaveMedia(new AddMediaDto()
            {
                Media = dto.Image
            });
            await _bannerService.Update(id, new UpdateBannerDto()
            {
                Extension = media.Extension,
                URL = media.URL,
                ImageName = media.ImageName,
                Title = dto.Title,
                UniqueName = media.UniqueName
            });
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }


    }

    private static void StopIfBannerNotFound(Banner? banner)
    {
        if (banner == null)
        {
            throw new BannerNotFoundException();
        }
    }
}
