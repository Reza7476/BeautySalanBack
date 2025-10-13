using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Extensions;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Dtos;
using BeautySalon.Services.Users.Exceptions;

namespace BeautySalon.Services.Users;
public class UserAppService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserAppService(
        IUserRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        this._unitOfWork = unitOfWork;
    }

    public async Task<string> Add(AddUserDto dto)
    {
        dto.Mobile = PhoneNumberExtensions.NormalizePhoneNumber(dto.Mobile);

        if (await _repository.IsExistByUserName(dto.UserName))
        {
            throw new UserNameIsDuplicateException();
        }

        if (await _repository.IsExistByMobileNumber(dto.Mobile))
        {
            throw new MobileNumberIsDuplicateException();
        }

        var hasPass = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            HashPass = hasPass,
            Email = dto.Email,
            Mobile = dto.Mobile,
            IsActive = true,
            LastName = dto.LastName,
            Name = dto.Name,
            UserName = dto.UserName,
            CreationDate = DateTime.UtcNow,
        };

        await _repository.Add(user);
        await _unitOfWork.Complete();
        return user.Id;
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _repository.IsExistByUserName(userName);
    }
}
