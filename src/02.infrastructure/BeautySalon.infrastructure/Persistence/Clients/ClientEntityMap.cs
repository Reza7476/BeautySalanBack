using BeautySalon.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Clients;
public class ClientEntityMap : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> _)
    {
        _.ToTable("Users").HasKey("Id");
        
        _.Property(_ => _.Id).IsRequired();

        _.Property(_ => _.UserId).IsRequired();
    }
}
