using BeautySalon.Application.Users.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Application.Users.Contracts;
public interface IUserHandle : IScope
{
    Task EnsureAdminIsExist(string adminUser, string adminPass);
    Task<GetTokenDto> Login(LoginDto dto);
    Task<string> RefreshToken(string refreshToken);
}
