namespace BeautySalon.Common.Interfaces;
public interface IEventPublisher : IScope
{
    void Publish<TEvent>(TEvent @event);
}
