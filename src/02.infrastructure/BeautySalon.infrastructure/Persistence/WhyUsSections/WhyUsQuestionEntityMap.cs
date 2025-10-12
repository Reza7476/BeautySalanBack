using BeautySalon.Entities.WhyUsSections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.WhyUsSections;
public class WhyUsQuestionEntityMap : IEntityTypeConfiguration<Why_Us_Question>
{
    public void Configure(EntityTypeBuilder<Why_Us_Question> _)
    {
        _.ToTable("Why_Us_Questions").HasKey(_ => _.Id);
       
        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        
        _.Property(_ => _.Question).IsRequired();
        
        _.Property(_ => _.Answer).IsRequired();
        
        _.Property(_ => _.SectionId).IsRequired();
        
        _.HasOne(_ => _.Section)
            .WithMany(_ => _.Why_Us_Questions)
            .HasForeignKey(_ => _.SectionId)
            .IsRequired();
    }
}

