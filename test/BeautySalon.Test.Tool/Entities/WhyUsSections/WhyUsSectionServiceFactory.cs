using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.WhyUsSections;
using BeautySalon.Services.WhyUsSections;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public static class WhyUsSectionServiceFactory
{
    public static WhyUsSectionAppService Generate(EFDataContext context)
    {
        var unitOfWork = new EFUnitOfWork(context);
        var whyUsSectionRepository = new EFWhyUsSectionRepository(context);
        
        return new WhyUsSectionAppService(whyUsSectionRepository, unitOfWork);
    }
}
