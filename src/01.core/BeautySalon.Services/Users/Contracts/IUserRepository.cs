using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;

namespace BeautySalon.Services.Users.Contracts;
public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<bool> IsExistByMobileNumber(string mobile);
    Task<bool> IsExistByUserName(string userName);
}
