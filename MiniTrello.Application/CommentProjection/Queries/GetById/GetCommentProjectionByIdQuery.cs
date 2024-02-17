using MediatR;
using MiniTrello.Contracts.CommentProjection;

namespace MiniTrello.Application.CommentProjection.Queries.GetById;

public sealed record GetCommentProjectionByIdQuery(Guid CommentId) : IRequest<CommentProjectionResponse>;
