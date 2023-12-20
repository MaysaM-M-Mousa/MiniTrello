using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.UnitTests.Aggregates.Builders;

public class TicketBuilder
{
    private List<IDomainEvent> _events;
    private Guid _ticketId;

    public TicketBuilder()
    {
        _events = new();
        _ticketId = Guid.NewGuid();
    }

    public TicketBuilder AddCreatedEvent()
    {
        _events.Add(new TicketCreatedDomainEvent(_ticketId, string.Empty, Priority.None, TicketStatus.ToDo));
        return this;
    }

    public TicketBuilder AddAssignedEvent()
    {
        _events.Add(new TicketAssignedDomainEvent(_ticketId, "Maysam"));
        return this;
    }

    public TicketBuilder AddMovedToInProgressEvent()
    {
        _events.Add(new TicketMovedToInProgressDomainEvent(_ticketId));
        return this;
    }

    public TicketBuilder AddMovedToCodeReviewEvent()
    {
        _events.Add(new TicketMovedToCodeReviewDomainEvent(_ticketId));
        return this;
    }

    public TicketBuilder AddMovedToTestEvent()
    {
        _events.Add(new TicketMovedToTestDomainEvent(_ticketId));
        return this;
    }

    public TicketBuilder AddMovedToDoneEvent()
    {
        _events.Add(new TicketMovedToDoneDomainEvent(_ticketId));
        return this;
    }

    public TicketBuilder AddDeletedEvent()
    {
        _events.Add(new TicketDeletedDomainEvent(_ticketId));
        return this;
    }

    public Ticket Build()
    {
        var ticket = Ticket.Load(_ticketId, _events);

        _events.Clear();
        
        return ticket;
    }
}
