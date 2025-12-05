namespace BeautySalon.Common.Interfaces;
public interface IFireBaseAuthService : IScope
{
    Task<string> GetAccessTokenAsync();
}
