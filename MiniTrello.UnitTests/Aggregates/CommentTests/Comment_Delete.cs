﻿using FluentAssertions;
using MiniTrello.Domain.Ticket.Comment.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.CommentTests;

public class Comment_Delete
{
    [Fact]
    public void DeleteComment_Succeeds()
    {
        var comment = new CommentBuilder().BuildNewlyAddedComment();

        var result = comment.DeleteComment();

        result.IsSuccess.Should().BeTrue();
        comment.UncommittedEvents.Count.Should().Be(1);
        comment.UncommittedEvents.Single().Should().BeOfType(typeof(CommentDeletedDomainEvent));
    }

    [Fact]
    public void DeletingAlready_DeletedTicket_Fails()
    {
        var comment = new CommentBuilder().BuildDeletedComment();

        var result = comment.DeleteComment();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("MiniTrello.Comment.DeletedComment");
    }
}