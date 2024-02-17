using MiniTrello.Domain.Ticket.Comment.Projections;

namespace MiniTrello.Infrastructure.Persistence.Repositories;

internal class InMemoryCommentProjectionRepository : ICommentProjectionRepository
{
    private readonly List<CommentProjection> _projections = new();

    public Task<List<CommentProjection>> GetAll()
    {
        return Task.FromResult(_projections);
    }

    public Task<CommentProjection?> GetProjectionById(Guid commentId)
    {
        var result = _projections
            .Where(p => p.CommentId == commentId)
            .FirstOrDefault();

        return Task.FromResult(result);
    }

    public async Task SaveProjection(CommentProjection commentProjection)
    {
        var dbProjection = await GetProjectionById(commentProjection.CommentId);

        if (dbProjection is null)
        {
            _projections.Add(commentProjection);
            return;
        }

        _projections.Remove(dbProjection);
        _projections.Add(commentProjection);
    }
}
