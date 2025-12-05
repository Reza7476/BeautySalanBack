using BeautySalon.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Notifications;
public class NotificationEntityMap : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> _)
    {
        _.ToTable("Notifications").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired();

        _.Property(_ => _.Title).IsRequired();

        _.Property(_ => _.Body).IsRequired();

        _.Property(_=>_.Receiver).IsRequired(); 

        _.Property(_=>_.Type).IsRequired();

        _.Property(_=>_.FCMToken).IsRequired();

        _.Property(_=>_.UserId).IsRequired();

        _.Property(_ => _.IsSent);

        _.Property(_ => _.CreatedAt);

        _.Property(_ => _.SentAt);
    }
}
