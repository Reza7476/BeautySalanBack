using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.RefreshTokens;
using BeautySalon.Services.RefreshTokens.Contacts.Dtos;

namespace BeautySalon.Services.RefreshTokens.Contacts;
public interface IRefreshTokenRepository : IRepository
{
    Task Add(RefreshToken refreshToken);
    Task<RefreshToken?> FindByToken(string refreshToken);
    Task<GetRefreshTokenDto?> GetTokenInfo(string refreshToken);
}
