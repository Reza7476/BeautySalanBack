using BeautySalon.Entities.WeeklySchedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.WeeklySchedules;
public class WeeklyScheduleEntityMap : IEntityTypeConfiguration<WeeklySchedule>
{
    public void Configure(EntityTypeBuilder<WeeklySchedule> _)
    {
        _.ToTable("WeeklySchedules").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

        _.Property(_ => _.DayOfWeek).IsRequired();
        
        _.Property(_ => _.StartTime).IsRequired();
        
        _.Property(_ => _.EndTime).IsRequired();
        
        _.Property(_ => _.IsActive);
    }
}
