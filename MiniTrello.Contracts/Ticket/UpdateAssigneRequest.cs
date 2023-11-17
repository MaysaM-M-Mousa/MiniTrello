namespace MiniTrello.Contracts.Ticket;

public class UpdateAssigneRequest
{
    public Guid TicketId { get; set; }

    public string User { get; set; }
}
