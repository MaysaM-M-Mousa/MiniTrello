namespace MiniTrello.Contracts.CommentProjection;

public sealed  class CommentProjectionResponse
{
    public Guid CommentId { get; set; }

    public Guid TicketId { get; set; }

    public string Content { get; set; } = null!;

    public string Commentator { get; set; } = null!;

    public bool IsDeleted => DeleteOnUtc.HasValue;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public DateTime? DeleteOnUtc { get; set; }
}
