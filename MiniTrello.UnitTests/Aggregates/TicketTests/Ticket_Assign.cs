using FluentAssertions;
using FluentAssertions.Execution;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_Assign
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void AssigneTicketTo_NullOrEmptyString_Fails(string user)
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();

        var result = ticket.Assign(user);

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("MiniTrello.Ticket.InvalidUser");
        }
    }

    [Fact]
    public void AssigneTicketTo_ValidUser_Succeeds()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();
        var user = "Maysam";

        var result = ticket.Assign(user);

        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            ticket.Assignee.Should().Be(user);
            ticket.UncommittedEvents.Count.Should().Be(1);
            ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketAssignedDomainEvent));
        }
    }

    [Fact]
    public void AssigneTicketTo_DeletedTicket_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();
        var user = "Maysam";
        
        var result = ticket.Assign(user);

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
        }
    }
}
