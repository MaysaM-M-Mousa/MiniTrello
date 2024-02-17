using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket.Comment.DomainEvents;

namespace MiniTrello.Domain.Ticket.Comment.Projections;

public class CommentProjection : IProjection
{
    public Guid CommentId{ get; set; }

    public Guid TicketId { get; set; }

    public string Content { get; set; } = null!;

    public string Commentator { get; set; } = null!;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public DateTime? DeleteOnUtc { get; set; }

    public void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case CommentAddedDomainEvent:
                Apply((CommentAddedDomainEvent)@event);
                break;
            case CommentContentModifiedDomainEvent:
                Apply((CommentContentModifiedDomainEvent)@event);
                break;
            case CommentDeletedDomainEvent:
                Apply((CommentDeletedDomainEvent)@event);
                break;
            default:
                throw new Exception($"Unsupported Event type {@event.GetType().Name}");
        }
    }

    public void Apply(CommentAddedDomainEvent @event) 
    {
        CommentId = @event.AggregateId;
        TicketId = @event.TicketId;
        Content = @event.Content;
        Commentator = @event.User;
        CreatedOnUtc = @event.OccurredAt;
    }

    public void Apply(CommentContentModifiedDomainEvent @event) 
    {
        Content = @event.Content;
        ModifiedOnUtc = @event.OccurredAt;
    }

    public void Apply(CommentDeletedDomainEvent @event)
    {
        DeleteOnUtc = @event.OccurredAt;
    }
}
