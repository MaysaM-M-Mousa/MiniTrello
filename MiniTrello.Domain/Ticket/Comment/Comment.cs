using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Primitives.Result;
using MiniTrello.Domain.Ticket.Comment.DomainEvents;

namespace MiniTrello.Domain.Ticket.Comment;

public class Comment : AggregateRoot
{
    public Guid TicketId { get; private set; }

    public bool IsDeleted { get; private set; }

    private Comment() { }

    private Comment(Guid aggregateId, Guid ticketId) : base(aggregateId)
    {
        TicketId = ticketId;
    }

    public static Result<Comment> AddComment(Guid ticketId, string user, string text)
    {
        var comment = new Comment(Guid.NewGuid(), ticketId);

        var @event = new CommentAddedDomainEvent(comment.AggregateId, ticketId, user, text);

        comment.AddUncommittedEvent(@event);
        comment.Apply(@event);

        return comment;
    }

    public Result DeleteComment() 
    {
        if (IsDeleted)
        {
            return CommentErrors.DeletedComment();
        }

        var @event = new CommentDeletedDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
    }

    public static Comment Load(Guid commentId, Guid ticketId, List<IDomainEvent> events)
    {
        var comment = new Comment(commentId, ticketId);

        foreach (var @event in events)
        {
            comment.When(@event);
        }

        return comment;
    }

    public override void When(IDomainEvent @event) 
    {
        switch (@event)
        {
            case CommentAddedDomainEvent:
                Apply((CommentAddedDomainEvent)@event);
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

    }

    public void Apply(CommentDeletedDomainEvent @event)
    {
        IsDeleted = true;
    }
}