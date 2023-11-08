using FluentAssertions;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Create
{
    [Fact]
    public void CreateTicket()
    {
        var ticket = Ticket.Create();

        ticket.Assignee.Should().Be(string.Empty);
        ticket.Status.Should().Be(TicketStatus.ToDo);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketCreatedDomainEvent));
    }
}
