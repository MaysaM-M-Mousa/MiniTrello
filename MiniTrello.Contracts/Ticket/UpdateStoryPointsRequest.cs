namespace MiniTrello.Contracts.Ticket;

public class UpdateStoryPointsRequest
{
    public Guid TicketId { get; set; }
    public int StoryPoints { get; set; }
}
