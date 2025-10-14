using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.RefreshTokens;

namespace BeautySalon.Services.RefreshTokens.Contacts;
public interface IRefreshTokenRepository : IRepository
{
    Task Add(RefreshToken refreshToken);
}
