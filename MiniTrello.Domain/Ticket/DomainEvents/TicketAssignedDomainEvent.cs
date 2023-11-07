using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.DomainEvents;

public sealed record TicketAssignedDomainEvent(Guid AggregateId, string Assignee) : IDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
}
