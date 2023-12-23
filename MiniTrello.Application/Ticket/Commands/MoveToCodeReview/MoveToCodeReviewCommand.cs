using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.MoveToCodeReview;

public sealed record MoveToCodeReviewCommand(Guid TicketId) : IRequest<Result>;
