using FluentAssertions;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToTest
{
    [Fact]
    public void MoveingFromCodeReviewToTestSucceeds()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        ticket.MoveToTest();

        ticket.Status.Should().Be(TicketStatus.Test);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToTestDomainEvent));
    }

    [Fact]
    public void MovingFromTestToTestDoesNothing()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        ticket.MoveToTest();

        ticket.Status.Should().Be(TicketStatus.Test);
        ticket.UncommittedEvents.Should().BeEmpty();
    }

    [Fact]
    public void MovingFromToDoToTestFails()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        var act = () => ticket.MoveToTest();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only CodeReview tickets can be moved to Test!");
    }
}
