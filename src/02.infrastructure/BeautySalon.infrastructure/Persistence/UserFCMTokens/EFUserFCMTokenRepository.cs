using BeautySalon.Entities.Users;
using BeautySalon.Services.UserFCMTokens.Contract;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.UserFCMTokens;
public class EFUserFCMTokenRepository : IUserFCMTokenRepository
{

    private readonly DbSet<UserFCMToken> _userFCMTokens;

    public EFUserFCMTokenRepository(EFDataContext context)
    {
        _userFCMTokens = context.Set<UserFCMToken>();
    }

    public async Task Add(UserFCMToken userFCMToken)
    {
        await _userFCMTokens.AddAsync(userFCMToken);
    }

    public async Task<bool> IsExistByFCMTokenAndUserIdAndIsActive(string fCMToken, string userId)
    {

        return await _userFCMTokens.AnyAsync(_ =>
            _.FCMToken == fCMToken && 
            _.UserId == userId && 
            _.IsActive);
    }
}
