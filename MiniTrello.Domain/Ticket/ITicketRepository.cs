using MiniTrello.Domain.Primitives;

public interface ITicketRepository
{
    Task SaveEventsAsync(Guid aggregateId, List<IDomainEvent> events);

    Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId);
}