using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Comment.ModifyComment;

public sealed record ModifyCommentCommand(Guid CommentId, string content) : IRequest<Result>;
