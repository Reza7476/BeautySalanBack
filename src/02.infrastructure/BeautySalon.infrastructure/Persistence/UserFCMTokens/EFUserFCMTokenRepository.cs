using BeautySalon.Entities.Roles;
using BeautySalon.Entities.Users;
using BeautySalon.Services.UserFCMTokens.Contract;
using BeautySalon.Services.UserFCMTokens.Contract.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.UserFCMTokens;
public class EFUserFCMTokenRepository : IUserFCMTokenRepository
{

    private readonly DbSet<UserFCMToken> _userFCMTokens;
    private readonly DbSet<User> _users;
    private readonly DbSet<UserRole> _userRoles;
    private readonly DbSet<Role> _roles;


    public EFUserFCMTokenRepository(EFDataContext context)
    {
        _userFCMTokens = context.Set<UserFCMToken>();
        _users = context.Set<User>();
        _userRoles = context.Set<UserRole>();
        _roles = context.Set<Role>();
    }

    public async Task Add(UserFCMToken userFCMToken)
    {
        await _userFCMTokens.AddAsync(userFCMToken);
    }

    public async Task AddRange(List<UserFCMToken> userFCMTokens)
    {
        await _userFCMTokens.AddRangeAsync(userFCMTokens);
    }

    public async Task<List<GetFCMTokenForSendNotificationDto>> GetAdminsToken()
    {
        return await _userFCMTokens.Select(_ => new GetFCMTokenForSendNotificationDto()
        {
            Token = _.FCMToken
        }).ToListAsync();
    }

    public async Task<List<string>> GetUserRolesByUserId(string userId)
    {
        var query = await _userRoles
            .Where(_ => _.UserId == userId)
            .Include(_ => _.Role)
            .Select(_ => _.Role.RoleName)
            .Distinct()
            .ToListAsync();
        return query;
    }

    public async Task<bool> IsExistByFCMTokenAndUserIdAndIsActiveAndRole(string fCMToken, string userId, string role)
    {
        return await _userFCMTokens.AnyAsync(_ =>
            _.FCMToken == fCMToken &&
            _.UserId == userId &&
            _.Role == role &&
            _.IsActive);
    }
}
