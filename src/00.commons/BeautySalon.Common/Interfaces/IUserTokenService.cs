namespace BeautySalon.Common.Interfaces;
public interface IUserTokenService : IScope
{
    string? UserId { get; }     
    string? UserName { get; }

}
