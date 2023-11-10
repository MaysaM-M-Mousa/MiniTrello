using MediatR;

namespace MiniTrello.Application.Ticket.Commands.MoveToDone;

public sealed record MoveToDoneCommand(Guid TicketId) : IRequest;
