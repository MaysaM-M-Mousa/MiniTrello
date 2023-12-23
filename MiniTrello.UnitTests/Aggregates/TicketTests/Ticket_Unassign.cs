using FluentAssertions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Unassign
{
    [Fact]
    public void UnassignAlready_UnassignedTicket_Fails()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        var result = ticket.Unassign();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("MiniTrello.Ticket.UnassignedTicket");
    }

    [Fact]
    public void UnassignAlready_AssignedTicket_Succeeds()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        var result = ticket.Unassign();

        result.IsSuccess.Should().BeTrue();
        ticket.Assignee.Should().Be(string.Empty);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketUnassignedDomainEvent));
    }

    [Fact]
    public void UnassignDeletedTicket_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var result = ticket.Unassign();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
    }
}
