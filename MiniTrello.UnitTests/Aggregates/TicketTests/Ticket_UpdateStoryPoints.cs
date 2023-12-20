using FluentAssertions;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_UpdateStoryPoints
{
    [Fact]
    public void UpdateStoryPointsShould_Succeeds()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        ticket.UpdateStoryPoints(5);

        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketStoryPointsUpdatedDomainEvent));
    }

    [Fact]
    public void UpdatingDeletedTicketStoryPoints_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var act = () => ticket.UpdateStoryPoints(5);

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Can't Perform actions on deleted ticket!");
    }
}
