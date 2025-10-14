using BeautySalon.Entities.RefreshTokens;
using BeautySalon.Services.RefreshTokens.Contacts;
using BeautySalon.Services.RefreshTokens.Contacts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.RefreshTokens;
public class EFRefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _refreshToken;


    public EFRefreshTokenRepository(EFDataContext context)
    {
        _refreshToken = context.Set<RefreshToken>();
    }

    public async Task Add(RefreshToken refreshToken)
    {
        await _refreshToken.AddAsync(refreshToken);
    }

    public async Task<GetRefreshTokenDto?> GetTokenInfo(string refreshToken)
    {
        return await _refreshToken
            .Where(_ => _.Token == refreshToken).Select(_ => new GetRefreshTokenDto()
            {
                ExpireAt = _.ExpireAt,
                IsRevoked = _.IsRevoked,
                Token = _.Token,
                UserId = _.UserId
            }).FirstOrDefaultAsync();
    }
}
