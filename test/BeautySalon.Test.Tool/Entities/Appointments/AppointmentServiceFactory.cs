using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.Appointments;
using BeautySalon.Services.Appointments;

namespace BeautySalon.Test.Tool.Entities.Appointments;
public static class AppointmentServiceFactory
{
    public static AppointmentAppService Generate(EFDataContext context)
    {

        var repository = new EFAppointmentRepository(context);
        var unitOfWork = new EFUnitOfWork(context);

        return new AppointmentAppService(repository,unitOfWork);
    }
}
