using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket.Comment;
using MiniTrello.Domain.Ticket.Comment.DomainEvents;

namespace MiniTrello.UnitTests.Aggregates.Builders;

public class CommentBuilder
{
    private List<IDomainEvent> _events;
    private Guid _ticketId;
    private Guid _commentId;

    public CommentBuilder()
    {
        _events = new();
        _ticketId = Guid.NewGuid();
        _commentId = Guid.NewGuid();
    }

    public CommentBuilder AddCommentAddedEvent()
    {
        _events.Add(new CommentAddedDomainEvent(_commentId, _ticketId, "Maysam", "Some comment content!"));
        return this;
    }

    public CommentBuilder AddCommentDeletedEvent()
    {
        _events.Add(new CommentDeletedDomainEvent(_commentId));
        return this;
    }

    public Comment Build()
    {
        var comment = Comment.Load(_commentId, _events);

        _events.Clear();

        return comment;
    }
}
