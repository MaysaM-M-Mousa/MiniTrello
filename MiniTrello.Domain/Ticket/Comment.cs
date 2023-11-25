namespace MiniTrello.Domain.Ticket;

public class Comment
{
    public long Id { get; private set; }

    public Guid TicketId { get; private set; }

    public string Text { get; private set; }

    public string User { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    private Comment() { }

    private Comment(Guid ticketId, string text, DateTime createdOnUtc, string user)
    {
        TicketId = ticketId;
        Text = text;
        User = user;
        CreatedOnUtc = createdOnUtc;
    }

    public static Comment CreateComment(Guid ticketId, string text, string user)
    {
        return new Comment(ticketId, text, DateTime.UtcNow, user);
    }
}