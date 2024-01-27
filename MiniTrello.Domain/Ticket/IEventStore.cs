using MiniTrello.Domain.Primitives;

public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId, List<IDomainEvent> events);

    Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId);
}