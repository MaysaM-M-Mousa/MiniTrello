using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Domain.Ticket;

public static class TicketErrors
{
    public static Error DeletedTicket(string? message = null) => 
        new Error("MiniTrello.Ticket.DeletedTicket", 
            message ?? "Can't perform actions on a deleted ticket!");

    public static Error InvalidAssigneeName(string? message = null) => 
        new Error("MiniTrello.Ticket.InvalidUser",
            message ?? "Empty or null user name!");

    public static Error UnassignedTicket(string? message = null) => 
        new Error("MiniTrello.Ticket.UnassignedTicket", 
            message ?? "Ticket already unassigned!");

    public static Error AlreadyInProgress(string? message = null) =>
        new Error("MiniTrello.Ticket.AlreadyInProgress",
            message ?? "Ticket already in progress!");

    public static Error AlreadyInCodeReview(string? message = null) =>
        new Error("MiniTrello.Ticket.AlreadyInCodeReview",
            message ?? "Ticket already in code review!");

    public static Error AlreadyInTest(string? message = null) =>
        new Error("MiniTrello.Ticket.AlreadyInTest",
            message ?? "Ticket already in test!");

    public static Error AlreadyInDone(string? message = null) =>
        new Error("MiniTrello.Ticket.AlreadyInDone",
            message ?? "Ticket already in done!");

    public static Error InvalidTicketOperation(string? message = null) =>
        new Error("MiniTrello.Ticket.InvalidOperation",
            message ?? "Can't perform invalid ticket operation");
}
