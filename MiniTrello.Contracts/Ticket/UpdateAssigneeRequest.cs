namespace MiniTrello.Contracts.Ticket;

public class UpdateAssigneeRequest
{
    public Guid TicketId { get; set; }

    public string User { get; set; }
}
