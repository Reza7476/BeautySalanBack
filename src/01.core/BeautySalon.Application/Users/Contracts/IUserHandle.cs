using BeautySalon.Application.Users.Contracts.Dtos;
using BeautySalon.Application.Users.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Application.Users.Contracts;
public interface IUserHandle : IScope
{
    Task EnsureAdminIsExist(string adminUser, string adminPass);

    Task<GetTokenDto> FinalizingRegister(
        FinalizingRegisterUserHandlerDto dto);

    Task<ResponseInitializeRegisterUserHandlerDto> InitializeRegister(InitializeRegisterUserDto dto);
    Task<GetTokenDto> Login(LoginDto dto);
    Task<GetTokenDto> RefreshToken(string refreshToken);
}
