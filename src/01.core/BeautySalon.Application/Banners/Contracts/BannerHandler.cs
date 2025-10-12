using BeautySalon.Application.Banners.Contracts.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.Application.Banners.Contracts;
public interface BannerHandler : IScope
{
    Task<long> Add(AddBannerHandlerDto dto);
    Task UpdateBanner(long id, UpdateBannerHandlerDto dto);
}
