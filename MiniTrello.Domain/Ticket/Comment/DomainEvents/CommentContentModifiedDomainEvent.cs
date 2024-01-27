using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.Comment.DomainEvents;

public sealed record CommentContentModifiedDomainEvent(Guid AggregateId, string Text) : IDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    Guid IDomainEvent.AggregateId => AggregateId;

    public DateTime OccurredAt { get; set; } =  DateTime.UtcNow;
}
