using BeautySalon.Entities.Technicians;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Technicians;
public class TechnicianEntityMap : IEntityTypeConfiguration<Technician>
{
    public void Configure(EntityTypeBuilder<Technician> _)
    {
        _.ToTable("Technicians").HasKey(_=>_.Id);

        _.Property(_ => _.Id).IsRequired();

        _.Property(_ => _.UserId).IsRequired();

        _.Property(_ => _.CreatedDate);
    }
}
