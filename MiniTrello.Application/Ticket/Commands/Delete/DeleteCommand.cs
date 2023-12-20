using MediatR;

namespace MiniTrello.Application.Ticket.Commands.Delete;

public sealed record DeleteCommand(Guid TicketId) : IRequest;
