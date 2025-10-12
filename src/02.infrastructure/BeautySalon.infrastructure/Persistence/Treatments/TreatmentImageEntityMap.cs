using BeautySalon.Entities.Treatments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Treatments;
public class TreatmentImageEntityMap : IEntityTypeConfiguration<TreatmentImage>
{
    public void Configure(EntityTypeBuilder<TreatmentImage> _)
    {
        _.ToTable("TreatmentImages").HasKey(_ => _.Id);
       
        _.Property(_ => _.Id).IsRequired();
        
        _.Property(_=>_.URL).IsRequired();
        
        _.Property(_=>_.ImageName).IsRequired();
        
        _.Property(_=>_.ImageUniqueName).IsRequired();
        
        _.Property(_=>_.CreateDate).IsRequired();
        
        _.Property(_=>_.TreatmentId).IsRequired();
        
        _.HasOne(_ => _.Treatment)
            .WithMany(_ => _.Images)
            .HasForeignKey(_ => _.TreatmentId)
            .IsRequired();
    }
}
