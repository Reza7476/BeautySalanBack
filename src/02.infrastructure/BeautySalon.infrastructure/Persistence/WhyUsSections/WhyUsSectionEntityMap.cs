using BeautySalon.Entities.WhyUsSections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.WhyUsSections;
public class WhyUsSectionEntityMap : IEntityTypeConfiguration<Why_Us_Section>
{
    public void Configure(EntityTypeBuilder<Why_Us_Section> _)
    {
        _.ToTable("Why_Us_Sections").HasKey(_ => _.Id);
        
        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

        _.Property(_ => _.Title).IsRequired().IsRequired();
        
        _.Property(_ => _.Description).IsRequired().IsRequired();
        
        _.Property(_ => _.CreateDate).IsRequired();

        _.OwnsOne(x => x.Image, image =>
        {
            image.Property(media => media.UniqueName)
                 .HasColumnName("ImageUniqueName")
                 .IsRequired();

            image.Property(media => media.ImageName)
                 .HasColumnName("ImageName")
                 .IsRequired();

            image.Property(media => media.Extension)
                 .HasColumnName("Extension")
                 .IsRequired();

            image.Property(media => media.URL)
                 .HasColumnName("URL")
                 .IsRequired();
        });

    }
}
