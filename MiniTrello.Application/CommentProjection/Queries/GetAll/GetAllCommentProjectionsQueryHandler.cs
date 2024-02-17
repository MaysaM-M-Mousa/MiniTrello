using MediatR;
using MiniTrello.Contracts.CommentProjection;
using MiniTrello.Contracts.Common;
using MiniTrello.Domain.Ticket.Comment.Projections;

namespace MiniTrello.Application.CommentProjection.Queries.GetAll;

internal sealed class GetAllCommentProjectionsQueryHandler : IRequestHandler<GetAllCommentProjectionsQuery, ListResponse<CommentProjectionResponse>>
{
    private readonly ICommentProjectionRepository _commentProjectionRepository;

    public GetAllCommentProjectionsQueryHandler(ICommentProjectionRepository commentProjectionRepository)
    {
        _commentProjectionRepository = commentProjectionRepository;
    }

    public async Task<ListResponse<CommentProjectionResponse>> Handle(GetAllCommentProjectionsQuery request, CancellationToken cancellationToken)
    {
        var projections = await _commentProjectionRepository.GetAll();

        return new ListResponse<CommentProjectionResponse>()
        {
            Entities = projections.Select(projection => new CommentProjectionResponse()
            {
                CommentId = projection.CommentId,
                TicketId = projection.TicketId,
                Commentator = projection.Commentator,
                Content = projection.Content,
                CreatedOnUtc = projection.CreatedOnUtc,
                ModifiedOnUtc = projection.ModifiedOnUtc,
                DeleteOnUtc = projection.DeleteOnUtc
            }).ToList()
        };
    }
}
