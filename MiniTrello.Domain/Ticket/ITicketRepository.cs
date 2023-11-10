using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket;

public interface ITicketRepository
{
    Task SaveEventsAsync(Guid aggregateId, List<IDomainEvent> events);

    Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId);
}
