using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.Treatments;
using BeautySalon.Services.Treatments;

namespace BeautySalon.Test.Tool.Entities.Treatments;
public static class TreatmentServiceFactory
{
    public static TreatmentAppService Generate(EFDataContext context)
    {
        var unitOfWork = new EFUnitOfWork(context);
        var treatmentRepository = new EFTreatmentRepository(context);

        return new TreatmentAppService(treatmentRepository, unitOfWork);
    }
}
