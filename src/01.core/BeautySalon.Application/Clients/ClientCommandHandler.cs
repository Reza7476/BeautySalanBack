using BeautySalon.Application.Clients.Contracts;
using BeautySalon.Application.Clients.Contracts.Dtos;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Extensions;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services;
using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Services.Clients.Contracts.Dtos;
using BeautySalon.Services.Extensions;
using BeautySalon.Services.Roles.Contracts;
using BeautySalon.Services.Roles.Contracts.Dtos;
using BeautySalon.Services.SMSLogs.Contracts;
using BeautySalon.Services.SMSLogs.Contracts.Dtos;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
using BeautySalon.Services.Users.Exceptions;

namespace BeautySalon.Application.Clients;
public class ClientCommandHandler : ClientHandler
{

    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IClientService _clientService;
    private readonly ISMSService _smsService;
    private readonly ISMSSetting _smsSetting;
    private readonly ISMSLogService _smsLogService;

    public ClientCommandHandler(IUserService userService,
        IRoleService roleService,
        IClientService clientService,
        ISMSService smsService,
        ISMSSetting smsSetting,
        ISMSLogService smsLogService)
    {
        _userService = userService;
        _roleService = roleService;
        _clientService = clientService;
        _smsService = smsService;
        _smsSetting = smsSetting;
        _smsLogService = smsLogService;
    }

    public async Task<string> AddNewClient(AddNewClientHandlerDto dto)
    {
        var mobile = PhoneNumberExtensions.NormalizePhoneNumber(dto.Mobile);
        if (await _userService.IsExistByMobileNumber(dto.Mobile))
        {
            throw new MobileNumberHasBeenRegisteredException();
        }
        var password = 4.GenerateOtpCode();
        var userId = await _userService.Add(new AddUserDto()
        {
            Mobile = mobile,
            Name = dto.Name,
            LastName = dto.LastName,
            UserName = dto.Mobile,
            Password = password,
        });
        var roleId = await _roleService.Add(new AddRoleDto()
        {
            RoleName = SystemRole.Client
        });
        await _roleService.AssignRoleToUser(userId, roleId);
        var clientId = await _clientService.Add(new AddClientDto()
        {
            UserId = userId
        });

        var sendSMS = await _smsService.SendSMS(new SendSMSDto()
        {
            BodyName = "RegisterClient",
            Number = dto.Mobile,
            Args = new List<string> { dto.Mobile, password.ToString() }
        });

        var smsLogId = await _smsLogService.Add(new AddSMSLogDto()
        {
            ErrorMessage = sendSMS.Status,
            Message = $"کاربر گرامی برای ورود به سایت سالن زیبایی از نام کاربری {dto.Mobile} و رمز عبور {password} استفاده کنید. لطفا بعد از ورود نسبت به تغییر رمز اقدام کنید . ",
            ProviderNumber = _smsSetting.SMSSettings.ProviderNumber,
            ReceiverNumber = dto.Mobile,
            RecId = sendSMS.RecId
        });
        var smsStatus = await _smsService.VerifySMS(sendSMS.RecId);

        return clientId;
    }
}
