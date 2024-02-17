using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.Comment.DomainEvents;

public sealed record CommentDeletedDomainEvent(
    Guid AggregateId) : ICommentDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    Guid IDomainEvent.AggregateId => AggregateId;

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
