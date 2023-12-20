using FluentAssertions;
using MiniTrello.Domain.Exceptions;
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

        var act = () => ticket.Assign(user);

        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void AssigneTicketTo_ValidUser_Succeeds()
    {
        var ticket = new TicketBuilder().BuildUnassignedTicket();
        var user = "Maysam";

        ticket.Assign(user);

        ticket.Assignee.Should().Be(user);
        ticket.UncommittedEvents.Count.Should().Be(1);
        ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketAssignedDomainEvent));
    }

    [Fact]
    public void AssigneTicketTo_DeletedTicket_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();
        var user = "Maysam";
        
        var act = () => ticket.Assign(user);

        act.Should()
            .Throw<MiniTrelloValidationException>()
            .WithMessage("Can't Perform actions on deleted ticket!");
    }
}
