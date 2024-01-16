using FluentAssertions;
using MiniTrello.Domain.Ticket.Comment;
using MiniTrello.Domain.Ticket.Comment.DomainEvents;

namespace MiniTrello.UnitTests.Aggregates.CommentTests;

public class Comment_Add
{
    [Fact]
    public void AddingNewComment_Succeeds()
    {
        var result = Comment.AddComment(Guid.NewGuid(), "Maysam", "Some content");

        result.IsSuccess.Should().BeTrue();
        var addComment = result.Value;
        addComment.UncommittedEvents.Count.Should().Be(1);
        addComment.UncommittedEvents.Single().Should().BeOfType(typeof(CommentAddedDomainEvent));
    }
}
