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

    public Ticket Build()
    {
        var ticket = Ticket.Load(_ticketId, _events);

        _events.Clear();
        
        return ticket;
    }
}
