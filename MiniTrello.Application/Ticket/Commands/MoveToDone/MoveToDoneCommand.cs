using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.MoveToDone;

public sealed record MoveToDoneCommand(Guid TicketId) : IRequest<Result>;
