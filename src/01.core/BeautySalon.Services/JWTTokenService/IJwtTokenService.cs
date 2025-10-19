using BeautySalon.Common.Interfaces;
using BeautySalon.Services.JWTTokenService.Contracts.Dtos;

namespace BeautySalon.Services.JWTTokenService;
public interface IJwtTokenService : IScope
{
    Task<string> GenerateToken(AddGenerateTokenDto dto);
}
