using FluentAssertions;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Create
{
    [Fact]
    public void CreateTicket()
    {
        var result = Ticket.Create();

        result.IsSuccess.Should().BeTrue();
        var createdTicket = result.Value;
        createdTicket.Assignee.Should().Be(string.Empty);
        createdTicket.Status.Should().Be(TicketStatus.ToDo);
        createdTicket.UncommittedEvents.Count.Should().Be(1);
        createdTicket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketCreatedDomainEvent));
    }
}
