using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.RefreshTokens;
using BeautySalon.Services.RefreshTokens.Contacts;
using BeautySalon.Services.RefreshTokens.Contacts.Dtos;

namespace BeautySalon.Services.RefreshTokens;
public class RefreshTokenAppService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenAppService(
        IRefreshTokenRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> GenerateToken(string userId)
    {
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        var expireAt = DateTime.UtcNow.AddDays(7);
        var refreshToken = new RefreshToken()
        {
            ExpireAt = expireAt,
            IsRevoked = false,
            Token = token,
            UserId = userId
        };

        await _repository.Add(refreshToken);
        await _unitOfWork.Complete();
        return token;

    }

    public async Task<GetRefreshTokenDto?> GetTokenInfo(string refreshToken)
    {
        return await _repository.GetTokenInfo(refreshToken);
    }
}
