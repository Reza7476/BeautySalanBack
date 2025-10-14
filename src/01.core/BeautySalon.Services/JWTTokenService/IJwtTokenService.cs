using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Services.JWTTokenService;
public interface IJwtTokenService : IScope
{
    Task<string> GenerateToken(GetUserForLoginDto dto);
}
