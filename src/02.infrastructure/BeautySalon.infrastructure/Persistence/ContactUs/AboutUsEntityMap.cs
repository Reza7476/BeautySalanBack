using BeautySalon.Entities.ContactUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.ContactUs;
public class AboutUsEntityMap : IEntityTypeConfiguration<AboutUs>
{
    public void Configure(EntityTypeBuilder<AboutUs> _)
    {
        _.ToTable("AboutUs").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

        _.Property(_ => _.MobileNumber).IsRequired();
        
        _.Property(_ => _.Telephone).IsRequired(false);
        
        _.Property(_ => _.Address).IsRequired(false);
        
        _.Property(_ => _.Latitude).IsRequired(false);
        
        _.Property(_ => _.Longitude).IsRequired(false);
        
        _.Property(_ => _.Description).IsRequired(false);

        _.Property(_ => _.CreateDate).IsRequired();

        _.Property(_ => _.Instagram).IsRequired(false);

        _.Property(_ => _.Email).IsRequired(false);

        _.OwnsOne(_ => _.LogoImage, image =>
        {
            image.Property(media => media.UniqueName)
                 .HasColumnName("UniqueName")
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
