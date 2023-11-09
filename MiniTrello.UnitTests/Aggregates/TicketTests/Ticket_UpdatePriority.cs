using FluentAssertions;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_UpdatePriority
{
    [Fact]
    public void UpdatePriorityShouldSucceeds()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        ticket.UpdatePriority(Priority.Hotfix);

        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketPriorityUpdatedDomainEvent));
    } 
}
