﻿using MiniTrello.Domain.Primitives;
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

    private Comment(Guid aggregateId) : base(aggregateId) { }

    public static Result<Comment> Create(Guid ticketId, string user, string content)
    {
        var comment = new Comment(Guid.NewGuid(), ticketId);

        var @event = new CommentAddedDomainEvent(comment.AggregateId, ticketId, user, content);

        comment.AddUncommittedEvent(@event);
        comment.Apply(@event);

        return comment;
    }

    public Result ModifyContent(string content)
    {
        if (IsDeleted)
        {
            return CommentErrors.DeletedComment();
        }

        var @event = new CommentContentModifiedDomainEvent(AggregateId, content);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
    }

    public Result Delete() 
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

    public static Comment Load(Guid commentId, List<IDomainEvent> events)
    {
        var comment = new Comment(commentId);

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

    public void Apply(CommentAddedDomainEvent @event) { }

    public void Apply(CommentDeletedDomainEvent @event)
    {
        IsDeleted = true;
    }

    public void Apply(CommentContentModifiedDomainEvent @event) { }
}