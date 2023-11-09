using FluentAssertions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.Domain.Ticket;
using MiniTrello.UnitTests.Aggregates.Builders;
using MiniTrello.Domain.Exceptions;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToCodeReview
{
    [Fact]
    public void MovingFrom_InProgress_To_CodeReview_Succeeds()
    {
        var ticket = new TicketBuilder().BuildInProgressStatusTicket();

        ticket.MoveToCodeReview();

        ticket.Status.Should().Be(TicketStatus.CodeReview);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToCodeReviewDomainEvent));
    }

    [Fact]
    public void MovingFrom_CodeReview_To_CodeReview_DoesNothing()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        ticket.MoveToCodeReview();

        ticket.Status.Should().Be(TicketStatus.CodeReview);
        ticket.UncommittedEvents.Should().BeEmpty();
    }

    [Fact]
    public void MovingFrom_Test_To_CodeReview_Fails()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        var act = () => ticket.MoveToCodeReview();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only InProgress tickets can be moved to CodeReview!");
    }
}
