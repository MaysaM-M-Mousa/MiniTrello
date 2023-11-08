using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.UnitTests.Aggregates.Builders;

public static class TicketBuilder
{
    public static Ticket CreateUnassignedTicket()
    {
        var ticketId = Guid.NewGuid();

        var events = new List<IDomainEvent>()
        {
            new TicketCreatedDomainEvent(ticketId, string.Empty, Priority.None, TicketStatus.ToDo)
        };

        return Ticket.Load(ticketId, events);
    }

    public static Ticket CreateAssignedTicket()
    {
        var ticketId = Guid.NewGuid();

        var events = new List<IDomainEvent>()
        {
            new TicketCreatedDomainEvent(ticketId, string.Empty, Priority.None, TicketStatus.ToDo),
            new TicketAssignedDomainEvent(ticketId, "Maysam")
        };

        return Ticket.Load(ticketId, events);
    }
}
