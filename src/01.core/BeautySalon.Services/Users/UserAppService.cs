using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Commons;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Clients.Exceptions;
using BeautySalon.Services.Extensions;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
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

    public async Task ChangePassword(string newPassword, string mobile)
    {
        var user = await _repository.FindByMobile(mobile);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var hashPass = BCrypt.Net.BCrypt.HashPassword(newPassword);

        user.HashPass = hashPass;
        await _unitOfWork.Complete();
    }

    public async Task ChangeUserActivation(ChangeUserActivationDto dto, string userId)
    {

        var user=await _repository.FindById(dto.UserId);
        if(user== null) 
        {
            throw new UserNotFoundException();
        }
        
        if (user.Id == userId)
        {
            throw new YouAreNotAllowedToAccessException();  
        }

        user.IsActive = dto.IsActive;
        await _unitOfWork.Complete();   

    }

    public async Task EditAdminProfile(EditAdminProfileDto dto, string? id)
    {

        if (id == null)
        {
            throw new YouAreNotAllowedToAccessException();
        }
        var user = await _repository.FindById(id);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        user.BirthDate = dto.BirthDate;
        user.Email = dto.Email;
        user.Name = dto.Name;
        user.LastName = dto.LastName;
        await _unitOfWork.Complete();
    }

    public async Task EditClientProfile(EditClientProfileDto dto, string id)
    {
        var user = await _repository.FindById(id);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        if (await _repository.IsExistByUserNameExceptItSelf(dto.UserName, id))
        {
            throw new UserNameIsDuplicateException();
        }
        user.BirthDate = dto.BirthDateGregorian;
        user.Name = dto.Name;
        user.LastName = dto.LastName;
        user.UserName = dto.UserName;
        user.Email = dto.Email;
        await _unitOfWork.Complete();
    }

    public async Task EditImageProfile(ImageDetailsDto media, string id)
    {
        var user = await _repository.FindById(id);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (user.Avatar == null)
            user.Avatar = new MediaDocument();

        user.Avatar.ImageName = media.ImageName;
        user.Avatar.UniqueName = media.UniqueName;
        user.Avatar.URL = media.URL;
        user.Avatar.Extension = media.Extension;

        await _unitOfWork.Complete();
    }

    public async Task<IPageResult<GetAllUsersDto>> GetAllUsers(
        IPagination? pagination = null,
        string? search = null)
    {
        return await _repository.GetAllUsers(pagination,search);
    }

    public async Task<GetUserForLoginDto?> GetByUserIdForRefreshToken(string userId)
    {
        return await _repository.GetByUserIdForRefreshToken(userId);
    }

    public async Task<GetUserForLoginDto?> GetByUserNameForLogin(string userName)
    {
        return await _repository.GetByUserNameForLogin(userName);
    }

    public async Task<User?> GetUserForEditProfileImage(string id)
    {
        return await _repository.FindById(id);
    }

    public async Task<string?> GetUserIdByMobileNumber(string mobileNumber)
    {
        return await _repository.GetUserIdByMobileNumber(mobileNumber);
    }

    public async Task<GetUserInfoDto?> GetUserInfo(string? userId)
    {

        if (userId == null)
        {
            throw new YouAreNotAllowedToAccessException();
        }
        return await _repository.GetUserInfoById(userId);

    }

    public async Task<bool> IsExistByMobileNumber(string mobileNumber)
    {
        return await _repository.IsExistByMobileNumber(mobileNumber);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _repository.IsExistByUserName(userName);
    }

    public async Task<bool> IsExistByUserNameExceptItSelf(string id, string userName)
    {
        return await _repository.IsExistByUserNameExceptItSelf(userName, id);
    }
}
