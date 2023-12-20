namespace MiniTrello.Contracts.TicketDetailsProjection;

public class TicketDetailsProjectionResponse
{
    public Guid TicketId { get; set; }

    public string Assignee { get; set; }

    public string Priority { get; set; }

    public int StoryPoints { get; set; }

    public string Status { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? StartedOnUtc { get; set; }

    public DateTime? CompletedOnUtc { get; set; }

    public DateTime? DeletedOnUtc { get; set; }
}
