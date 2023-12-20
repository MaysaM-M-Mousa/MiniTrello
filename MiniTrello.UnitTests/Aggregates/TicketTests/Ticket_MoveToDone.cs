using FluentAssertions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.Domain.Ticket;
using MiniTrello.UnitTests.Aggregates.Builders;
using MiniTrello.Domain.Exceptions;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToDone
{
    [Fact]
    public void MovingFrom_Test_To_Done_Succeeds()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        ticket.MoveToDone();

        ticket.Status.Should().Be(TicketStatus.Done);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToDoneDomainEvent));
    }

    [Fact]
    public void MovingFrom_Done_ToDone_DoesNothing()
    {
        var ticket = new TicketBuilder().BuildDoneStatusTicket();

        ticket.MoveToDone();

        ticket.Status.Should().Be(TicketStatus.Done);
        ticket.UncommittedEvents.Should().BeEmpty();
    }

    [Fact]
    public void MovingFrom_CodeReview_To_Done_DoesNothing()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        var act = () => ticket.MoveToDone();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only Test tickets can be moved to Done!");
    }

    [Fact]
    public void MovingDeletedTicket_To_Done_DoesNothing()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var act = () => ticket.MoveToDone();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Can't Perform actions on deleted ticket!");
    }
}
