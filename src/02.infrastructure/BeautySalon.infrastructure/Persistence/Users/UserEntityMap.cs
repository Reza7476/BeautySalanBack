using BeautySalon.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Users;
public class UserEntityMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> _)
    {
        _.ToTable("Users").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired();

        _.Property(_ => _.Name).IsRequired(false);

        _.Property(_ => _.LastName).IsRequired(false);

        _.Property(_ => _.Mobile).IsRequired(false);

        _.Property(_ => _.UserName).IsRequired(false);

        _.Property(_ => _.HashPass).IsRequired(false);

        _.Property(_ => _.Email).IsRequired(false);

        _.Property(_ => _.CreationDate).IsRequired();

        _.Property(_ => _.BirthDate).IsRequired(false);

        _.Property(_ => _.IsActive).IsRequired();

        _.OwnsOne(_ => _.Avatar, image =>
        {
            image.Property(media => media.UniqueName)
                 .HasColumnName("UniqueName")
                 .IsRequired(false);

            image.Property(media => media.ImageName)
                 .HasColumnName("ImageName")
                 .IsRequired(false);

            image.Property(media => media.Extension)
                 .HasColumnName("Extension")
                 .IsRequired(false);

            image.Property(media => media.URL)
                 .HasColumnName("URL")
                 .IsRequired(false);
        });
    }
}
