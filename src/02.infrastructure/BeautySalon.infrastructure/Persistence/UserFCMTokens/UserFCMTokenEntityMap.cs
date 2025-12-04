using BeautySalon.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.UserFCMTokens;
public class UserFCMTokenEntityMap : IEntityTypeConfiguration<UserFCMToken>
{
    public void Configure(EntityTypeBuilder<UserFCMToken> _)
    {
        _.ToTable("UserFCMTokens").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired();

        _.Property(_ => _.UserId).IsRequired();

        _.Property(_ => _.FCMToken).IsRequired();

        _.Property(_ => _.Role).IsRequired();

        _.Property(_ => _.CreatedAt);

        _.Property(_ => _.IsActive);

        _.HasOne(_ => _.User)
            .WithMany(_ => _.UserFCMTokens)
            .HasForeignKey(_ => _.UserId);
    }
}
