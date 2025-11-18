using BeautySalon.Entities.AppointmentReviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.AppointmentReviews;
public class AppointmentReviewsEntityMap : IEntityTypeConfiguration<AppointmentReview>
{
    public void Configure(EntityTypeBuilder<AppointmentReview> _)
    {
        _.ToTable("AppointmentReviews").HasKey(_=>_.Id);

        _.Property(_=>_.Id).IsRequired();

        _.Property(_=>_.AppointmentId).IsRequired();
        
        _.Property(_ => _.ClientId).IsRequired();
        
        _.Property(_ => _.TreatmentId).IsRequired();
        
        _.Property(_=>_.TechnicianId).IsRequired();
        
        _.Property(_ => _.Rate).IsRequired();
        
        _.Property(_=>_.Description).IsRequired(false);

        _.Property(_ => _.IsPublished);
        
        _.Property(_ => _.CreatedAt);

        _.HasOne(_ => _.Appointment)
            .WithOne(_ => _.Review)
            .HasForeignKey<AppointmentReview>(_ => _.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
