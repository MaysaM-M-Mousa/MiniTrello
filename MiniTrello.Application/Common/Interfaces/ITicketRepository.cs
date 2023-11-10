using MiniTrello.Domain.Primitives;

namespace MiniTrello.Application.Common.Interfaces;

public interface ITicketRepository
{
    Task SaveEventsAsync(Guid aggregateId, List<IDomainEvent> events);

    Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId);
}
