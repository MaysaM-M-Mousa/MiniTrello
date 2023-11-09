using FluentAssertions;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToInProgress
{
    [Fact]
    public void MovingFromToDoToInProgressSucceeds()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        ticket.MoveToInProgress();

        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToInProgressDomainEvent));
    }

    [Fact]
    public void MovingFromTestToInProgressSucceeds()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        ticket.MoveToInProgress();

        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToInProgressDomainEvent));
    }

    [Fact]
    public void MovingAlreadyInProgressTicketToInProgressDoesNothing()
    {
        var ticket = new TicketBuilder().BuildInProgressStatusTicket();

        ticket.MoveToInProgress();

        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Should().BeEmpty();
    }

    [Fact]
    public void MovingFromCodeReviewToInProgressFails()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        var act = () => ticket.MoveToInProgress();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only Ticket in ToDo or Test lists can be moved to InProgress list!");
    }

    [Fact]
    public void MovingFromDoneToInProgressFails()
    {
        var ticket = new TicketBuilder().BuildDoneStatusTicket();

        var act = () => ticket.MoveToInProgress();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Only Ticket in ToDo or Test lists can be moved to InProgress list!");
    }
}
