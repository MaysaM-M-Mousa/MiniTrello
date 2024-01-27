namespace MiniTrello.Contracts.Comment;

public sealed record AddCommentRequest
{
    public string User { get; set; }
    public string Content { get; set; }
}
