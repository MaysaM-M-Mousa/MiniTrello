using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.MoveToTest;

public sealed record MoveToTestCommand(Guid TicketId) : IRequest<Result>;
