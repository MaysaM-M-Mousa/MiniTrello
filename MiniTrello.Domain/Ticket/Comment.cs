namespace MiniTrello.Domain.Ticket;

public class Comment
{
    public int Id { get; private set; }

    public Guid TicketId { get; private set; }

    public string Text { get; private set; }

    public DateTime DateTime { get; private set; } = DateTime.UtcNow;

    public string User { get; private set; }
}