using FluentAssertions;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Unassign
{
    [Fact]
    public void UnassignAlreadyUnassignedTicketFails()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        var act = () => ticket.Unassign();

        act.Should()
            .Throw<TicketAlreadyUnassignedException>()
            .WithMessage("The ticket already unassigned");
    }

    [Fact]
    public void UnassignAlreadyAssignedTicketSucceeds()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        ticket.Unassign();

        ticket.Assignee.Should().Be(string.Empty);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketUnassignedDomainEvent));
    }
}
