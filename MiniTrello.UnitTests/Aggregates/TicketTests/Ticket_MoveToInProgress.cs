using FluentAssertions;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToInProgress
{
    [Fact]
    public void MovingFrom_ToDo_To_InProgress_Succeeds()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        ticket.MoveToInProgress();

        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToInProgressDomainEvent));
    }

    [Fact]
    public void MovingFrom_Test_To_InProgress_Succeeds()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        ticket.MoveToInProgress();

        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToInProgressDomainEvent));
    }

    [Fact]
    public void MovingAlreadyInProgressTicket_To_InProgress_DoesNothing()
    {
        var ticket = new TicketBuilder().BuildInProgressStatusTicket();

        ticket.MoveToInProgress();

        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Should().BeEmpty();
    }

    [Fact]
    public void MovingFrom_CodeReview_To_InProgress_Fails()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        var act = () => ticket.MoveToInProgress();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only Ticket in ToDo or Test lists can be moved to InProgress list!");
    }

    [Fact]
    public void MovingFrom_Done_To_InProgress_Fails()
    {
        var ticket = new TicketBuilder().BuildDoneStatusTicket();

        var act = () => ticket.MoveToInProgress();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only Ticket in ToDo or Test lists can be moved to InProgress list!");
    }
}
