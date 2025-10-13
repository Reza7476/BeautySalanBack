using BeautySalon.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Roles;
public class RoleEntityMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> _)
    {
        _.ToTable("Roles").HasKey(_ => _.Id);
        
        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        
        _.Property(_ => _.RoleName).IsRequired();
        
        _.Property(_ => _.CreationDate).IsRequired();

    }
}
