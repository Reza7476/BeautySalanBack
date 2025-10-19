using BeautySalon.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.OtpRequests;
public class OtpRequestEntityMap : IEntityTypeConfiguration<OtpRequest>
{
    public void Configure(EntityTypeBuilder<OtpRequest> _)
    {
        _.ToTable("OtpRequests").HasKey(t => t.Id);

        _.Property(_ => _.Id).IsRequired();

        _.Property(_ => _.Mobile).IsRequired();

        _.Property(_ => _.OtpCode).IsRequired();

        _.Property(_ => _.IsUsed).IsRequired();

        _.Property(_ => _.ExpireAt).IsRequired();

        _.Property(_ => _.CreatedAt).IsRequired();

        _.Property(_ => _.Purpose).IsRequired();
    }
}
