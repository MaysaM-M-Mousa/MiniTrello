namespace MiniTrello.Domain.Ticket.Comment.Projections;

public class CommentProjection
{
    public Guid CommentId{ get; set; }

    public Guid TicketId { get; set; }

    public string Content { get; set; }

    public string Commentator { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime ModifiedOnUtc { get; set; }

    public DateTime? DeleteOnUtc { get; set; }
}
