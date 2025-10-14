using BeautySalon.Entities.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.RefreshTokens;
public class RefreshTokenEntityMap : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> _)
    {
        _.ToTable("RefreshTokens").HasKey(_ => _.Id);
        
        _.Property(_ => _.Id).ValueGeneratedOnAdd();
        
        _.Property(_ => _.Token).IsRequired();
        
        _.Property(_ => _.ExpireAt).IsRequired();
        
        _.Property(_ => _.IsRevoked);
        
        _.Property(_ => _.UserId).IsRequired();
        
        _.HasOne(_ => _.User)
            .WithMany(_ => _.RefreshTokens)
            .HasForeignKey(_ => _.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
