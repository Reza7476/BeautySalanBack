namespace BeautySalon.Common.Interfaces;
public interface IFireBaseNotificationService : IScope
{
    Task<bool> SendNotificationAsync(
        string token,
        string title,
        string body,
        string reciever,
        string type);
}
