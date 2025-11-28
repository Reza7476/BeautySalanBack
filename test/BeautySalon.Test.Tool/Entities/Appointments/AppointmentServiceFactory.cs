using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.Appointments;
using BeautySalon.Services.Appointments;
using Moq;

namespace BeautySalon.Test.Tool.Entities.Appointments;
public static class AppointmentServiceFactory
{
    public static AppointmentAppService Generate(EFDataContext context,DateTime? fackeDateTime=null)
    {

        var repository = new EFAppointmentRepository(context);
        var unitOfWork = new EFUnitOfWork(context);
        var dateTime = new Mock<IDateTimeService>();
        dateTime.Setup(_=>_.Now).Returns(fackeDateTime??DateTime.Now);
        return new AppointmentAppService(repository,unitOfWork,dateTime.Object);
    }
}
