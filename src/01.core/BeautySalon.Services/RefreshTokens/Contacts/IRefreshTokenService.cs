using BeautySalon.Common.Interfaces;
using BeautySalon.Services.RefreshTokens.Contacts.Dtos;

namespace BeautySalon.Services.RefreshTokens.Contacts;
public interface IRefreshTokenService : IService
{
    Task<string> GenerateToken(string userId);
    Task<GetRefreshTokenDto?> GetTokenInfo(string refreshToken);
}
