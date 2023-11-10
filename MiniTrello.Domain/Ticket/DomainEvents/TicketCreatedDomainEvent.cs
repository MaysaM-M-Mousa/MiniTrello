using MediatR;
using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.DomainEvents;

public sealed record TicketCreatedDomainEvent(
    Guid AggregateId, 
    string Assignee, 
    Priority Priority, 
    TicketStatus Status) : IDomainEvent, INotification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
