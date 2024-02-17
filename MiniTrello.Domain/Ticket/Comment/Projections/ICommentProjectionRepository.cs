namespace MiniTrello.Domain.Ticket.Comment.Projections;

public interface ICommentProjectionRepository
{
    Task SaveProjection(CommentProjection commentProjection);

    Task<CommentProjection?> GetProjectionById(Guid commentId);

    Task<List<CommentProjection>> GetAll();
}
