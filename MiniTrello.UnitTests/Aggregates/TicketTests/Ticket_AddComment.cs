using FluentAssertions;
using MiniTrello.Domain.Ticket.Comment.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_AddComment
{
    [Fact]
    public void AddingComment_ToTicket_Succeeds()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        var result = ticket.AddComment("Maysam", "this is the ticket content!");

        result.IsSuccess.Should().BeTrue();
        var addedComment = result.Value;
        addedComment.UncommittedEvents.Single().Should().BeOfType(typeof(CommentAddedDomainEvent));
    }

    [Fact]
    public void AddingComment_ToDeletedTicket_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var result = ticket.AddComment("Maysam", "this is the ticket content!");

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
    }
}
