using FluentAssertions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.Domain.Ticket;
using MiniTrello.UnitTests.Aggregates.Builders;
using MiniTrello.Domain.Exceptions;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToDone
{
    [Fact]
    public void MoveFromTestToDoneSucceeds()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        ticket.MoveToDone();

        ticket.Status.Should().Be(TicketStatus.Done);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToDoneDomainEvent));
    }

    [Fact]
    public void MovingFromDoneToDoneDoesNothing()
    {
        var ticket = new TicketBuilder().BuildDoneStatusTicket();

        ticket.MoveToDone();

        ticket.Status.Should().Be(TicketStatus.Done);
        ticket.UncommittedEvents.Should().BeEmpty();
    }

    [Fact]
    public void MovingFromCodeReviewToDoneDoesNothing()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        var act = () => ticket.MoveToDone();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only Test tickets can be moved to Done!");
    }
}
