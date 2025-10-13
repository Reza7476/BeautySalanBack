using BeautySalon.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.Roles;
public class UserRoleEntityMap : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> _)
    {
        _.ToTable("UserRoles").HasKey(_ => _.Id);
        
        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        
        _.Property(_ => _.UserId).IsRequired();
        
        _.Property(_ => _.RoleId).IsRequired();

        _.HasOne(_ => _.User)
            .WithMany(_ => _.Roles)
            .HasForeignKey(_ => _.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        _.HasOne(_=>_.Role)
            .WithMany(_=>_.UserRoles)
            .HasForeignKey(_=>_.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
