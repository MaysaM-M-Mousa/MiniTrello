using FluentAssertions;
using FluentAssertions.Execution;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_UpdatePriority
{
    [Fact]
    public void UpdatePriorityShould_Succeeds()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        var result = ticket.UpdatePriority(Priority.Hotfix);

        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            ticket.UncommittedEvents.Count.Should().Be(1);
            ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketPriorityUpdatedDomainEvent));
        }
    }

    [Fact]
    public void UpdatingDeletedTicketPriority_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var result = ticket.UpdatePriority(Priority.Hotfix);

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
        }
    }
}
