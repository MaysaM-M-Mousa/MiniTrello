using MediatR;

namespace MiniTrello.Application.Ticket.Commands.UpdatePriority;

public sealed record UpdatePriorityCommand(Guid TicketId, string Priority) : IRequest;
