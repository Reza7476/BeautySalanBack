namespace BeautySalon.Common.Interfaces;
public interface INotificationJobs : IScope
{
    Task SendNewAppointmentNotification(string appointmentId);
}
