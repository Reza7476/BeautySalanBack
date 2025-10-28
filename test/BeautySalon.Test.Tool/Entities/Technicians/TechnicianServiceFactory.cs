using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.Technicians;
using BeautySalon.Services.Technicians;

namespace BeautySalon.Test.Tool.Entities.Technicians;
public static class TechnicianServiceFactory
{


    public static TechnicianAppService Generate(EFDataContext context)
    {
        var repository = new EFTechnicianRepository(context);

        var unitOfWork = new EFUnitOfWork(context);
        return new TechnicianAppService(repository, unitOfWork);
    }
}
