using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Domain.Ticket.Comment;

public static class CommentErrors
{
    public static Error DeletedComment(string? message = null) =>
        new Error("MiniTrello.Comment.DeletedComment",
            message ?? "Can't perform actions on a delete comment!");
}
