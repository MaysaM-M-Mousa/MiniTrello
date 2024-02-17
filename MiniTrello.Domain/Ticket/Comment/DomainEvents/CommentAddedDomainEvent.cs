using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.Comment.DomainEvents;

public sealed record CommentAddedDomainEvent(
    Guid AggregateId,
    Guid TicketId,
    string User,
    string Content) : ICommentDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    Guid IDomainEvent.AggregateId => AggregateId;

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
