using BeautySalon.Entities.RefreshTokens;
using BeautySalon.Services.RefreshTokens.Contacts;
using BeautySalon.Services.RefreshTokens.Contacts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.RefreshTokens;
public class EFRefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _refreshTokens;


    public EFRefreshTokenRepository(EFDataContext context)
    {
        _refreshTokens = context.Set<RefreshToken>();
    }

    public async Task Add(RefreshToken refreshToken)
    {
        await _refreshTokens.AddAsync(refreshToken);
    }

    public async Task<RefreshToken?> FindByToken(string refreshToken)
    {
        return await _refreshTokens.Where (_=>_.Token==refreshToken).FirstOrDefaultAsync();
    }

    public async Task<GetRefreshTokenDto?> GetTokenInfo(string refreshToken)
    {
        return await _refreshTokens
            .Where(_ => _.Token == refreshToken).Select(_ => new GetRefreshTokenDto()
            {
                ExpireAt = _.ExpireAt,
                IsRevoked = _.IsRevoked,
                Token = _.Token,
                UserId = _.UserId
            }).FirstOrDefaultAsync();
    }
}
