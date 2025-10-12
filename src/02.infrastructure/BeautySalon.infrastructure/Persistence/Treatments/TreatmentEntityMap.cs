using BeautySalon.Entities.Treatments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Treatments;
public class TreatmentEntityMap : IEntityTypeConfiguration<Treatment>
{
    public void Configure(EntityTypeBuilder<Treatment> _)
    {
        _.ToTable("Treatments").HasKey(_ => _.Id);
        
        _.Property(_=>_.Id).IsRequired().ValueGeneratedOnAdd();
        
        _.Property(_ => _.Title).IsRequired();
        
        _.Property(_ => _.Description).IsRequired();
       
        _.Property(_=>_.CreateDate).IsRequired();
    }
}
