using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Banners;
using BeautySalon.Services.Banners.Contracts;
using BeautySalon.Services.Banners.Contracts.Dto;
using BeautySalon.Services.Banners.Exceptions;

namespace BeautySalon.Services.Banners;
public class BannerAppService : IBannerService
{
    private readonly IBannerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public BannerAppService(
        IBannerRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Add(AddBannerDto dto)
    {
        var newBanner = new Banner()
        {
            CreateDate = DateTime.UtcNow,
            Extension = dto.Extension,
            ImageName = dto.ImageName,
            ImageUniqueName = dto.UniqueName,
            Title = dto.Title,
            URL = dto.URL,
        };

        await _repository.Add(newBanner);
        await _unitOfWork.Complete();

        return newBanner.Id;
    }

    public async Task<GetBannerDto?> Get()
    {
        return await _repository.GetBanner();
    }

    public async Task Update(long id, UpdateBannerDto dto)
    {
        var banner = await _repository.GetBannerById(id);

        StopIfBannerNotFound(banner);

        banner!.Title = dto.Title;
        banner.Extension = dto.Extension;
        banner.ImageName = dto.ImageName;
        banner.URL = dto.URL;
        banner.ImageUniqueName = dto.UniqueName;

        await _unitOfWork.Complete();
    }

    private static void StopIfBannerNotFound(Banner? banner)
    {
        if (banner == null)
        {
            throw new BannerNotFoundException();
        }
    }

    public Task<Banner?> GetById(long id)
    {
        return _repository.GetBannerById(id);
    }
}
