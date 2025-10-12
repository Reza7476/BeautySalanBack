using BeautySalon.Entities.Banners;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Banners;
public class BannerEntityMap : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> _)
    {
        _.ToTable("Banners").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

        _.Property(_ => _.Title).IsRequired();

        _.Property(_ => _.Extension).IsRequired();

        _.Property(_ => _.CreateDate).IsRequired();

        _.Property(_ => _.ImageName).IsRequired();
        
        _.Property(_ => _.URL).IsRequired();

    }
}
