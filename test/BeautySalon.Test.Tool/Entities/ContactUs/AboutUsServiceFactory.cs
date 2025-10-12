using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.ContactUs;
using BeautySalon.Services.ContactUs;

namespace BeautySalon.Test.Tool.Entities.ContactUs;
public static class AboutUsServiceFactory
{
    public static AboutUsAppService Generate(EFDataContext context)
    {
        var repository = new EFAboutUsRepository(context);
        var unitOfWork = new EFUnitOfWork(context);

        return new AboutUsAppService(repository, unitOfWork);
    }
}
