using BeautySalon.Application.Users.Contracts.Dtos;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Application.Users.Contracts;
public interface IUserHandle : IScope
{
    Task EditProfileImage(AddMediaDto dto, string userId);
    Task EnsureAdminIsExist(string adminUser, string adminPass);
    Task FinalizeResetPassword(ForgetPassStepTwoDto dto);
    Task<GetTokenDto> FinalizingRegister(
        FinalizingRegisterUserHandlerDto dto);
   
    Task<ResponseInitializeRegisterUserHandlerDto> 
        ForgetPasswordInitialize(InitializeRegisterUserDto dto);
   
    Task<ResponseInitializeRegisterUserHandlerDto> 
        InitializeRegister(InitializeRegisterUserDto dto);
    Task<GetTokenDto> Login(LoginDto dto);
    Task<GetTokenDto> RefreshToken(string refreshToken);
}
