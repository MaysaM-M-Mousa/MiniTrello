using FluentAssertions;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Delete
{
    [Fact]
    public void DeletingTicket_NotDeleting_Succeeds()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        ticket.Delete();

        ticket.IsDeleted.Should().BeTrue();
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketDeletedDomainEvent));
    }

    [Fact]
    public void DeletingTicket_AlreadyDeleted_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var act = () => ticket.Delete();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("This ticket already deleted!");
    }
}
