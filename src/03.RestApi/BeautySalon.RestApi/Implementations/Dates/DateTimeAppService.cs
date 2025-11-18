using BeautySalon.Common.Interfaces;

namespace BeautySalon.RestApi.Implementations.Dates;

public class DateTimeAppService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow; 

    public DateTime Today => DateTime.UtcNow.Date;
}
