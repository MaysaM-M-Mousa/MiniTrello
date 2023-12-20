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

    public static Ticket BuildInProgressStatusTicket(this TicketBuilder ticketBuilder)
    {
        return ticketBuilder
            .AddCreatedEvent()
            .AddAssignedEvent()
            .AddMovedToInProgressEvent()
            .Build();
    }

    public static Ticket BuildCodeReviewStatusTicket(this TicketBuilder ticketBuilder)
    {
        return ticketBuilder
            .AddCreatedEvent()
            .AddAssignedEvent()
            .AddMovedToInProgressEvent()
            .AddMovedToCodeReviewEvent()
            .Build();
    }

    public static Ticket BuildTestStatusTicket(this TicketBuilder ticketBuilder)
    {
        return ticketBuilder
            .AddCreatedEvent()
            .AddAssignedEvent()
            .AddMovedToInProgressEvent()
            .AddMovedToCodeReviewEvent()
            .AddMovedToTestEvent()
            .Build();
    }

    public static Ticket BuildDoneStatusTicket(this TicketBuilder ticketBuilder)
    {
        return ticketBuilder
            .AddCreatedEvent()
            .AddAssignedEvent()
            .AddMovedToInProgressEvent()
            .AddMovedToCodeReviewEvent()
            .AddMovedToTestEvent()
            .AddMovedToDoneEvent()
            .Build();
    }

    public static Ticket BuildDeletedTicket(this TicketBuilder ticketBuilder)
    {
        return ticketBuilder
            .AddCreatedEvent()
            .AddDeletedEvent()
            .Build();
    }
}
