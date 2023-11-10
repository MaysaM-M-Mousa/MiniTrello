using MediatR;

namespace MiniTrello.Application.Ticket.Commands.MoveToCodeReview;

public sealed record MoveToCodeReviewCommand(Guid TicketId) : IRequest;
