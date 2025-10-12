using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Banners;
using BeautySalon.Services.Banners.Contracts.Dto;

namespace BeautySalon.Services.Banners.Contracts;
public interface IBannerService : IScope
{
    Task<long> Add(AddBannerDto dto);
    Task<GetBannerDto?> Get();
    Task<Banner?> GetById(long id);
    Task Update(long id, UpdateBannerDto dto);
}
