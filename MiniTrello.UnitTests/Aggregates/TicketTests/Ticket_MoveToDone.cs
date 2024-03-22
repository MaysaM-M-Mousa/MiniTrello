using FluentAssertions;
using MiniTrello.Domain.Ticket.DomainEvents;
using MiniTrello.Domain.Ticket;
using MiniTrello.UnitTests.Aggregates.Builders;
using FluentAssertions.Execution;

namespace MiniTrello.UnitTests.Aggregates.TicketTests;

public class Ticket_MoveToDone
{
    [Fact]
    public void MovingFrom_Test_To_Done_Succeeds()
    {
        var ticket = new TicketBuilder().BuildTestStatusTicket();

        var result = ticket.MoveToDone();

        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            ticket.Status.Should().Be(TicketStatus.Done);
            ticket.UncommittedEvents.Count.Should().Be(1);
            ticket.UncommittedEvents.Single().Should().BeOfType(typeof(TicketMovedToDoneDomainEvent));
        }
    }

    [Fact]
    public void MovingFrom_Done_ToDone_Fails()
    {
        var ticket = new TicketBuilder().BuildDoneStatusTicket();

        var result = ticket.MoveToDone();

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            ticket.Status.Should().Be(TicketStatus.Done);
            ticket.UncommittedEvents.Should().BeEmpty();
        }
    }

    [Fact]
    public void MovingFrom_CodeReview_To_Done_Fails()
    {
        var ticket = new TicketBuilder().BuildCodeReviewStatusTicket();
        
        var result = ticket.MoveToDone();

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("MiniTrello.Ticket.InvalidOperation");
        }
    }

    [Fact]
    public void MovingDeletedTicket_To_Done_Fails()
    {
        var ticket = new TicketBuilder().BuildDeletedTicket();

        var result = ticket.MoveToDone();

        using (new AssertionScope())
        {
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("MiniTrello.Ticket.DeletedTicket");
        }
    }
}
