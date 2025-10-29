using BeautySalon.Entities.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Appointments;
public class AppointmentEntityMap : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> _)
    {
        _.ToTable("Appointments").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired();

        _.Property(_ => _.ClientId).IsRequired();

        _.Property(_ => _.TechnicianId).IsRequired();

        _.Property(_ => _.TreatmentId).IsRequired();

        _.Property(_ => _.AppointmentDate).IsRequired();

        _.Property(_ => _.EndTime).IsRequired();

        _.Property(_ => _.Duration).IsRequired();

        _.Property(_ => _.CancelledBy).IsRequired(false);
        
        _.Property(_ => _.CreatedAt).IsRequired();
        
        _.Property(_ => _.Status);

        _.Property(_ => _.CancelledAt);

        _.Property(_ => _.Description).IsRequired(false);

        _.HasOne(_ => _.Client)
            .WithMany(_ => _.Appointments)
            .HasForeignKey(_ => _.ClientId)
            .IsRequired();

        _.HasOne(_ => _.Technician)
            .WithMany(_ => _.Appointments)
            .HasForeignKey(_ => _.TechnicianId)
            .IsRequired();

        _.HasOne(_ => _.Treatment)
            .WithMany(_ => _.Appointments)
            .HasForeignKey(_ => _.TreatmentId)
            .IsRequired();
    }
}
