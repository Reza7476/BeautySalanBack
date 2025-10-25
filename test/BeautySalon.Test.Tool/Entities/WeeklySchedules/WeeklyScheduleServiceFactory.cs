using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.WeeklySchedules;
using BeautySalon.Services.WeeklySchedules;

namespace BeautySalon.Test.Tool.Entities.WeeklySchedules;
public static class WeeklyScheduleServiceFactory
{

    public static WeeklyScheduleAppService Generate(EFDataContext context)
    {
        var repository=new EFWeeklyScheduleRepository(context);
        var unitOfWork = new EFUnitOfWork(context);

        return new WeeklyScheduleAppService(repository, unitOfWork);
    }
}
