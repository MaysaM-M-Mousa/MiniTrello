using FluentAssertions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Assign
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void AssigneTicketToNullOrEmptyStringFails(string user)
    {
        var ticket = TicketBuilder.CreateUnassignedTicket();

        var act = () => ticket.Assign(user);

        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void AssigneTicketToValidUserSucceeds()
    {
        var ticket = TicketBuilder.CreateUnassignedTicket();
        var user = "Maysam";

        ticket.Assign(user);

        ticket.Assignee.Should().Be(user);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketAssignedDomainEvent));
    }
}
