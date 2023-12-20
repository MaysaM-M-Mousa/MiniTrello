using FluentAssertions;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Unassign
{
    [Fact]
    public void UnassignAlready_UnassignedTicket_Fails()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        var act = () => ticket.Unassign();

        act.Should()
            .Throw<TicketAlreadyUnassignedException>()
            .WithMessage("The ticket already unassigned");
    }

    [Fact]
    public void UnassignAlready_AssignedTicket_Succeeds()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        ticket.Unassign();

        ticket.Assignee.Should().Be(string.Empty);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketUnassignedDomainEvent));
    }

    [Fact]
    public void UnassignDeletedTicket_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var act = () => ticket.Unassign();

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Can't Perform actions on deleted ticket!");
    }
}
