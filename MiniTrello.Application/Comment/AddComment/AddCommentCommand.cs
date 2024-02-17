using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Comment.AddComment;

public sealed record AddCommentCommand(Guid TicketId, string User, string Content) : IRequest<Result>;

