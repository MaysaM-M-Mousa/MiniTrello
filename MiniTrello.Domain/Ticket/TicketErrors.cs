using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Domain.Ticket;

public static class TicketErrors
{
    public static Error DeletedTicket(string? message = null) => new Error("MiniTrello.Ticket.DeletedTicket", message ?? "Can't perform actions on a deleted ticket!");

    public static Error InvalidAssigneeName(string? message = null) => new Error("MiniTrello.Ticket.InvalidUser", message ?? "Empty or null user name!");

    public static Error UnassignedTicket(string? message = null) => new Error("MiniTrello.Ticket.UnassignedTicket", message ?? "Ticket already unassigned!");
}
