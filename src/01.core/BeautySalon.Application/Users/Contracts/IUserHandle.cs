using BeautySalon.Common.Interfaces;

namespace BeautySalon.Application.Users.Contracts;
public interface IUserHandle : IScope
{
    Task EnsureAdminIsExist(string adminUser, string adminPass);
}
