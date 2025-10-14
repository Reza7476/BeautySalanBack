using BeautySalon.Entities.RefreshTokens;
using BeautySalon.Services.RefreshTokens.Contacts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.RefreshTokens;
public class EFRefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _refreshToken;


    public EFRefreshTokenRepository(EFDataContext context)
    {
        _refreshToken=context.Set<RefreshToken>();
    }

    public async Task Add(RefreshToken refreshToken)
    {
        await _refreshToken.AddAsync(refreshToken);
    }
}
