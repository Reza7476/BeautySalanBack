using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Banners;
using BeautySalon.Services.Banners.Contracts.Dto;

namespace BeautySalon.Services.Banners.Contracts;
public interface IBannerRepository : IRepository
{
    Task Add(Banner newBanner);
    Task<GetBannerDto?> GetBanner();
    Task <Banner?> GetBannerById(long id);
}
