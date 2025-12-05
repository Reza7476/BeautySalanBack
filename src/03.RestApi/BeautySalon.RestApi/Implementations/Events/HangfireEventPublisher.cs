using BeautySalon.Application.Appointments.EventHandlers;
using BeautySalon.Common.Interfaces;
using Hangfire;

namespace BeautySalon.RestApi.Implementations.Events;

public class HangfireEventPublisher : IEventPublisher
{
    public void Publish<TEvent>(TEvent @event)
    {
        switch (@event)
        {
            case NewAppointmentCreatedEvent newAppointmentEvent:
                BackgroundJob.Enqueue<INotificationJobs>(
                    x => x.SendNewAppointmentNotification(newAppointmentEvent.AppointmentId));
                break;

                // میتوان Event های دیگر را نیز اینجا handle کرد
        }
    }
}
