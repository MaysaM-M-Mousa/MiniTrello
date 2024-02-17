using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Comment.DeleteComment;

public sealed record DeleteCommentCommand(Guid CommentId) : IRequest<Result>;
