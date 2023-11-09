using MiniTrello.Domain.Ticket;

namespace MiniTrello.UnitTests.Aggregates.Builders;

public static class TicketBuilderExtensions
{ 
    public static Ticket BuildUnassignedTicket(this TicketBuilder ticketBuilder)
    {
        return ticketBuilder
            .AddCreatedEvent()
            .Build();
    }

    public static Ticket BuildAssignedTicket(this TicketBuilder ticketBuilder)
    {
        return ticketBuilder
            .AddCreatedEvent()
            .AddAssignedEvent()
            .Build();
    }
}
