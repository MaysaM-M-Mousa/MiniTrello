using FluentAssertions;
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

        var result = ticket.MoveToInProgress();

        result.IsSuccess.Should().BeTrue();
        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToInProgressDomainEvent));
    }

    [Fact]
    public void MovingFrom_Test_To_InProgress_Succeeds()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        var result = ticket.MoveToInProgress();

        result.IsSuccess.Should().BeTrue();
        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToInProgressDomainEvent));
    }

    [Fact]
    public void MovingAlreadyInProgressTicket_To_InProgress_Fails()
    {
        var ticket = new TicketBuilder().BuildInProgressStatusTicket();

        var result = ticket.MoveToInProgress();

        result.IsFailure.Should().BeTrue();
        ticket.Status.Should().Be(TicketStatus.InProgress);
        ticket.UncommittedEvents.Should().BeEmpty();
    }

    [Fact]
    public void MovingFrom_CodeReview_To_InProgress_Fails()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        var result = ticket.MoveToInProgress();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("MiniTrello.Ticket.InvalidOperation");
    }

    [Fact]
    public void MovingFrom_Done_To_InProgress_Fails()
    {
        var ticket = new TicketBuilder().BuildDoneStatusTicket();

        var result = ticket.MoveToInProgress();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("MiniTrello.Ticket.InvalidOperation");
    }

    [Fact]
    public void Moving_UnassignedTicket_From_ToDo_To_InProgress_Fails()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        var result = ticket.MoveToInProgress();
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("MiniTrello.Ticket.UnassignedTicket");
    }

    [Fact]
    public void MovingDeleteTicket_To_InProgress_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var result = ticket.MoveToInProgress();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
    }
}
