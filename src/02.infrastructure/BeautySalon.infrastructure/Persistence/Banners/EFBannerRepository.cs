using BeautySalon.Entities.Banners;
using BeautySalon.Services.Banners.Contracts;
using BeautySalon.Services.Banners.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Banners;
public class EFBannerRepository : IBannerRepository
{
    private readonly DbSet<Banner> _banners;

    public EFBannerRepository(EFDataContext context)
    {
        _banners = context.Set<Banner>();

    }

    public async Task Add(Banner newBanner)
    {
        await _banners.AddAsync(newBanner);
    }

    public async Task<GetBannerDto?> GetBanner()
    {

        return await _banners.Select(_ => new GetBannerDto()
        {
            Extension = _.Extension,
            Id = _.Id,
            Title = _.Title,
            CreateDate = _.CreateDate,
            URL = _.URL,
            ImageName = _.ImageName,
            ImageUniqueName = _.ImageUniqueName
        }).FirstOrDefaultAsync();
    }

    public async Task<Banner?> GetBannerById(long id)
    {
        return await _banners.Where(_ => _.Id == id).FirstOrDefaultAsync();
    }
}
