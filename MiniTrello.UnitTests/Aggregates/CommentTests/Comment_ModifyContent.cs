using FluentAssertions;
using MiniTrello.Domain.Ticket.Comment.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.CommentTests;

public class Comment_ModifyContent
{
    [Fact]
    public void ModifyingCommentContent_Succeeds()
    {
        var comment = new CommentBuilder().BuildNewlyAddedComment();

        var result = comment.ModifyCommentContent("new comment content!");

        result.IsSuccess.Should().BeTrue();
        comment.UncommittedEvents.Single().Should().BeOfType(typeof(CommentContentModifiedDomainEvent));
    }

    [Fact]
    public void ModifyingCommentContent_ForDeletedTicket_Fails()
    {
        var comment = new CommentBuilder().BuildDeletedComment();

        var result = comment.ModifyCommentContent("new comment content!");

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("MiniTrello.Comment.DeletedComment");
        comment.UncommittedEvents.Count.Should().Be(0);
    }
}
