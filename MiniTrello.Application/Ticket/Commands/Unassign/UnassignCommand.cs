using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.Unassign;

public sealed record UnassignCommand(Guid TicketId) : IRequest<Result>;
