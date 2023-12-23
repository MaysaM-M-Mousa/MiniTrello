using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.Delete;

public sealed record DeleteCommand(Guid TicketId) : IRequest<Result>;
