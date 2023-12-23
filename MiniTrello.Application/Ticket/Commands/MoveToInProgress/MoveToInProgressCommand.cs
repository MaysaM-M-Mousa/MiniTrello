using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.MoveToInProgress;

public sealed record MoveToInProgressCommand(Guid TicketId) : IRequest<Result>;
