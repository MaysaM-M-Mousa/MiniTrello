using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Primitives;

namespace MiniTrello.Infrastructure.Persistence.Repositories;

internal sealed class InMemoryTicketRepository : ITicketRepository
{
    private readonly Dictionary<Guid, List<IDomainEvent>> _events = new();

    public Task<List<IDomainEvent>> GetEventsAsync(Guid aggregateId)
    {
        var isAggregateExists = _events.Keys.Contains(aggregateId);

        if (!isAggregateExists)
        {
            throw new MiniTrelloNotFoundException(nameof(aggregateId));
        }

        var result = Task.FromResult(_events[aggregateId]
            .OrderBy(e => e.OccurredAt)
            .ToList());

        return result;
    }

    public Task SaveEventsAsync(Guid aggregateId, List<IDomainEvent> events)
    {
        var isAggregateExists = _events.Keys.Contains(aggregateId);

        if (!isAggregateExists)
        {
            _events[aggregateId] = events;
            return Task.CompletedTask;
        }

        _events[aggregateId] = _events[aggregateId].Concat(events).ToList();

        return Task.CompletedTask;
    }
}
