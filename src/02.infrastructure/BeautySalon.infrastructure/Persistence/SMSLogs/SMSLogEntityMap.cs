using BeautySalon.Entities.SMSLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.SMSLogs;
public class SMSLogEntityMap : IEntityTypeConfiguration<SMSLog>
{
    public void Configure(EntityTypeBuilder<SMSLog> _)
    {
        _.ToTable("SMSLogs").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired();

        _.Property(_ => _.ReceiverNumber).IsRequired();

        _.Property(_ => _.Message).IsRequired();

        _.Property(_ => _.Status).IsRequired();

        _.Property(_=>_.RecId).IsRequired();

        _.Property(_ => _.ErrorMessage).IsRequired(false);

        _.Property(_ => _.CreatedAt).IsRequired();

        _.Property(_ => _.ProviderNumber).IsRequired();
    }
}
