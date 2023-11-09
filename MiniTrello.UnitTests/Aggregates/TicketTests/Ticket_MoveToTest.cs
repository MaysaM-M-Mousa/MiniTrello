using FluentAssertions;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToTest
{
    [Fact]
    public void MoveingFrom_CodeReview_To_Test_Succeeds()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        ticket.MoveToTest();

        ticket.Status.Should().Be(TicketStatus.Test);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToTestDomainEvent));
    }

    [Fact]
    public void MovingFrom_Test_To_Test_DoesNothing()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        ticket.MoveToTest();

        ticket.Status.Should().Be(TicketStatus.Test);
        ticket.UncommittedEvents.Should().BeEmpty();
    }

    [Fact]
    public void MovingFrom_ToDo_ToT_est_Fails()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        var act = () => ticket.MoveToTest();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only CodeReview tickets can be moved to Test!");
    }
}
