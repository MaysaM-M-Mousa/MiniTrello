using MediatR;
using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.DomainEvents;

public sealed record TicketAssignedDomainEvent(
    Guid AggregateId, 
    string Assignee) : IDomainEvent, INotification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
