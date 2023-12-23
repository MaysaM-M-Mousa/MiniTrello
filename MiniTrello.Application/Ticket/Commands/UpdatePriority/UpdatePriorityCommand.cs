using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.UpdatePriority;

public sealed record UpdatePriorityCommand(Guid TicketId, string Priority) : IRequest<Result>;
