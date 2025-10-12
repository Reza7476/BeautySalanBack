using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.Banners;
using BeautySalon.Services.Banners;

namespace BeautySalon.Test.Tool.Entities.Banners;
public static class BannerServiceFactory
{
    public static BannerAppService Generate(EFDataContext context)
    {

        var unitOfWork = new EFUnitOfWork(context);
        var bannerRepository = new EFBannerRepository(context);
        return new BannerAppService(bannerRepository, unitOfWork);
    }
}
