using MediatR;
using MiniTrello.Contracts.CommentProjection;
using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Ticket.Comment.Projections;

namespace MiniTrello.Application.CommentProjection.Queries.GetById;

internal sealed class GetCommentProjectionByIdQueryHandler : IRequestHandler<GetCommentProjectionByIdQuery, CommentProjectionResponse>
{
    private readonly ICommentProjectionRepository _commentProjectionRepository;

    public GetCommentProjectionByIdQueryHandler(ICommentProjectionRepository commentProjectionRepository)
    {
        _commentProjectionRepository = commentProjectionRepository;
    }

    public async Task<CommentProjectionResponse> Handle(GetCommentProjectionByIdQuery request, CancellationToken cancellationToken)
    {
        var projection = await _commentProjectionRepository.GetProjectionById(request.CommentId) 
            ?? throw new MiniTrelloNotFoundException(request.CommentId.ToString());

        return new()
        {
            CommentId = projection.CommentId,
            TicketId = projection.TicketId,
            Commentator = projection.Commentator,
            Content = projection.Content,
            CreatedOnUtc = projection.CreatedOnUtc,
            ModifiedOnUtc = projection.ModifiedOnUtc,
            DeleteOnUtc = projection.DeleteOnUtc,
        };
    }
}
