using BeautySalon.Common.Interfaces;
using System.Security.Claims;

namespace BeautySalon.RestApi.Implementations;

public class UserTokenAppService : IUserTokenService
{
    private readonly IHttpContextAccessor _accessor;

    public UserTokenAppService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }


    public string? UserId => GetUserIdFromJwtToken();

    public string? UserName => throw new NotImplementedException();



    private string? GetUserIdFromJwtToken()
    {
        return _accessor.HttpContext?.User.Claims
            .FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
