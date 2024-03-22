using FluentAssertions;
using FluentAssertions.Execution;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Delete
{
    [Fact]
    public void DeletingTicket_NotDeleting_Succeeds()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        var result = ticket.Delete();

        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            ticket.IsDeleted.Should().BeTrue();
            ticket.UncommittedEvents.Count.Should().Be(1);
            ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketDeletedDomainEvent));
        }
    }

    [Fact]
    public void DeletingTicket_AlreadyDeleted_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var result = ticket.Delete();

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
        }
    }
}
