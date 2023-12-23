using FluentAssertions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_UpdateStoryPoints
{
    [Fact]
    public void UpdateStoryPointsShould_Succeeds()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        var result = ticket.UpdateStoryPoints(5);

        result.IsSuccess.Should().BeTrue();
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketStoryPointsUpdatedDomainEvent));
    }

    [Fact]
    public void UpdatingDeletedTicketStoryPoints_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var result = ticket.UpdateStoryPoints(5);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
    }
}
