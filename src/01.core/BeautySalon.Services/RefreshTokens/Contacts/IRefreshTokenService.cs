using BeautySalon.Common.Interfaces;

namespace BeautySalon.Services.RefreshTokens.Contacts;
public interface IRefreshTokenService : IService
{
    Task<string> GenerateToken(string userId);
}
