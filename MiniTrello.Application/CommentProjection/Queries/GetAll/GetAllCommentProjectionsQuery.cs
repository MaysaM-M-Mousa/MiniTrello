using MediatR;
using MiniTrello.Contracts.CommentProjection;
using MiniTrello.Contracts.Common;

namespace MiniTrello.Application.CommentProjection.Queries.GetAll;

public sealed record GetAllCommentProjectionsQuery : IRequest<ListResponse<CommentProjectionResponse>>;
