using FluentAssertions;
using FluentAssertions.Execution;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.UnitTests.Aggregates.Builders;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToTest
{
    [Fact]
    public void MoveingFrom_CodeReview_To_Test_Succeeds()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();

        var result = ticket.MoveToTest();

        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            ticket.Status.Should().Be(TicketStatus.Test);
            ticket.UncommittedEvents.Count.Should().Be(1);
            ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToTestDomainEvent));
        }
    }

    [Fact]
    public void MovingFrom_Test_To_Test_Fails()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        var result = ticket.MoveToTest();

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            ticket.Status.Should().Be(TicketStatus.Test);
            ticket.UncommittedEvents.Should().BeEmpty();
        }
    }

    [Fact]
    public void MovingFrom_ToDo_To_Test_Fails()
    {
        var ticket = new TicketBuilder().BuildAssignedTicket();

        var result = ticket.MoveToTest();

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("MiniTrello.Ticket.InvalidOperation");
        }
    }

    [Fact]
    public void MovingDeletedTicket_To_Test_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var result = ticket.MoveToTest();

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
        }
    }
}
