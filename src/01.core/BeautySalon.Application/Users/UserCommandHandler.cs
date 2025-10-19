using BeautySalon.Application.Users.Contracts;
using BeautySalon.Application.Users.Contracts.Dtos;
using BeautySalon.Application.Users.Dtos;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Extensions;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.SMSLogs;
using BeautySalon.Entities.Users;
using BeautySalon.Services;
using BeautySalon.Services.Extensions;
using BeautySalon.Services.JWTTokenService;
using BeautySalon.Services.JWTTokenService.Contracts.Dtos;
using BeautySalon.Services.OTPRequests.Contacts;
using BeautySalon.Services.OTPRequests.Contacts.Dtos;
using BeautySalon.Services.OTPRequests.Exceptions;
using BeautySalon.Services.RefreshTokens.Contacts;
using BeautySalon.Services.RefreshTokens.Exceptions;
using BeautySalon.Services.Roles.Contracts;
using BeautySalon.Services.Roles.Contracts.Dtos;
using BeautySalon.Services.SMSLogs.Contracts;
using BeautySalon.Services.SMSLogs.Contracts.Dtos;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
using BeautySalon.Services.Users.Dtos;
using BeautySalon.Services.Users.Exceptions;

namespace BeautySalon.Application.Users;
public class UserCommandHandler : IUserHandle
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    private readonly ISMSService _smsService;
    private readonly ISMSLogService _smsLogService;
    private readonly ISMSSetting _smsSetting;
    private readonly IOtpRequestService _otpService;
    private readonly IMediaService _mediaService;

    public UserCommandHandler(IUserService userService,
        IRoleService roleService,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService,
        ISMSService smsService,
        ISMSLogService smsLogService,
        ISMSSetting smsSetting,
        IOtpRequestService otpService,
        IMediaService mediaService)
    {
        _userService = userService;
        _roleService = roleService;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _smsService = smsService;
        _smsLogService = smsLogService;
        _smsSetting = smsSetting;
        _otpService = otpService;
        _mediaService = mediaService;
    }

    public async Task EnsureAdminIsExist(string adminUser, string adminPass)
    {

        if (!await _userService.IsExistByUserName(adminUser))
        {
            var adminId = await CreateAdmin(adminUser, adminPass);
            var roleId = await CreateAdministratorAsRole();
            await AssignRoleToUser(adminId, roleId);
        }
    }

    public async Task<GetTokenDto> FinalizingRegister(FinalizingRegisterUserHandlerDto dto)
    {
        var otpRequest = await _otpService.GetByIdForRegister(dto.OtpRequestId);
        if (otpRequest == null)
        {
            throw new OtpCodeIsInvalidException();
        }

        if (otpRequest.OtpCode != dto.OtpCode)
        {
            throw new OtpCodeIsInvalidException();
        }

        if (DateTime.UtcNow > otpRequest.ExpireAt)
        {
            throw new OtpCodeExpiredException();
        }

        if (await _userService.IsExistByUserName(dto.UserName))
        {
            throw new UserNameIsDuplicateException();
        }

        await _otpService.ChangeIsUsedOtp(dto.OtpRequestId);
        var userId = await _userService.Add(new AddUserDto()
        {
            Email = dto.Email,
            LastName = dto.LastName,
            Mobile = otpRequest.Mobile,
            Name = dto.Name,
            Password = dto.Password,
            UserName = dto.UserName,
        });

        var roleId = await _roleService.Add(new AddRoleDto()
        {
            RoleName = SystemRole.Client
        });

        await _roleService.AssignRoleToUser(userId, roleId);

        var token = await _jwtTokenService.GenerateToken(new AddGenerateTokenDto()
        {
            Email = dto.Email,
            Id = userId,
            LastName = dto.LastName,
            Mobile = otpRequest.Mobile,
            Name = dto.Name,
            UserName = dto.UserName,
            UserRoles = new List<string>() { SystemRole.Client },

        });
        
        var refreshToken = await _refreshTokenService.GenerateToken(userId);
        return new GetTokenDto()
        {
            JwtToken = token,
            RefreshToken = refreshToken,
        };
    }

    public async Task<ResponseInitializeRegisterUserHandlerDto>
        InitializeRegister(InitializeRegisterUserDto dto)
    {
        string otpRequest = string.Empty;
        dto.MobileNumber = PhoneNumberExtensions.NormalizePhoneNumber(dto.MobileNumber);
        if (await _userService.IsExistByMobileNumber(dto.MobileNumber))
        {
            throw new MobileNumberHasBeenRegisteredException();
        }
        var otpCode = 6.GenerateOtpCode();
        var message = $"کد ثبت نام در سایت سالن زیبایی{otpCode}";

        var send = await _smsService.SendSMS(new SendSMSDto()
        {
            Message = message,
            Number = dto.MobileNumber
        });

       var smsLogId= await _smsLogService.Add(new AddSMSLogDto()
        {
            ErrorMessage = send.Status,
            Message = message,
            ProviderNumber = _smsSetting.SMSSettings.ProviderNumber,
            ReceiverNumber = dto.MobileNumber,
            RecId = send.RecId
        });

        var smsStatus = await _smsService.VerifySMS(send.RecId);

        if (smsStatus.ResultsAsCode.Contains(1) || smsStatus.Status == "عملیات موفق")
        {
            otpRequest = await _otpService.Add(new AddOTPRequestDto()
            {
                ExpireAt = DateTime.UtcNow.AddSeconds(120),
                IsUsed = false,
                Mobile = dto.MobileNumber,
                OtpCode = otpCode,
                Purpose = OtpPurpose.Register,
            });

            await _smsLogService.ChangeStatus(smsLogId, SendSMSStatus.Sent);
        }

        return new ResponseInitializeRegisterUserHandlerDto()
        {
            OtpRequestId = otpRequest,
            VerifyStatus = smsStatus.Status,
            VerifyStatusCode = smsStatus.ResultsAsCode.FirstOrDefault()
        };
    }

    public async Task<GetTokenDto> Login(LoginDto dto)
    {
        var user = await _userService.GetByUserNameForLogin(dto.UserName);

        if (user == null)
        {
            throw new UserNotFoundException();
        }
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.HashPass))
        {
            throw new UserNotFoundException();
        }
        var token = await _jwtTokenService.GenerateToken(new AddGenerateTokenDto()
        {
            Email = user.Email,
            Id = user.Id,
            LastName = user.LastName,
            Mobile = user.Mobile,
            Name = user.Name,
            UserRoles = user.UserRoles,
            UserName=user.UserName
        });
        var refreshToken = await _refreshTokenService.GenerateToken(user.Id!);

        return new GetTokenDto()
        {
            JwtToken = token,
            RefreshToken = refreshToken,
        };
    }

    public async Task<GetTokenDto> RefreshToken(string refreshToken)
    {
        var oldToken = await _refreshTokenService.GetTokenInfo(refreshToken);
        if (oldToken == null)
        {
            throw new TokenIsExpiredException();
        }

        if (oldToken.IsRevoked)
        {
            throw new TokenIsExpiredException();
        }
        var user = await _userService.GetByUserIdForRefreshToken(oldToken.UserId);

        if(user == null)
        {
            throw new UserNotFoundException();
        }
        var token = await _jwtTokenService.GenerateToken(new AddGenerateTokenDto()
        {
            UserName=user.UserName,
            UserRoles=user.UserRoles,
            Email= user.Email   ,
            Id= user.Id,
            Name=user.Name,
            LastName= user.LastName,
            Mobile = user.Mobile,   
        });
        return new GetTokenDto()
        {
            JwtToken = token,
            RefreshToken = refreshToken,
        };
    }

    private async Task AssignRoleToUser(
        string adminId,
        long roleId)
    {
        await _roleService.AssignRoleToUser(adminId, roleId);
    }

    private async Task<string> CreateAdmin(string adminUser, string adminPass)
    {
        var userId = await _userService.Add(new AddUserDto()
        {
            Name = "Sahar",
            LastName = "Dehghani",
            Email = "example@Gmail.com",
            Mobile = "+989038049565",
            UserName = adminUser,
            Password = adminPass
        });
        return userId;
    }

    private async Task<long> CreateAdministratorAsRole()
    {
        var roleDto = new AddRoleDto()
        {
            RoleName = "Admin",
        };

        var roleId = await _roleService.Add(roleDto);
        return roleId;
    }
}
