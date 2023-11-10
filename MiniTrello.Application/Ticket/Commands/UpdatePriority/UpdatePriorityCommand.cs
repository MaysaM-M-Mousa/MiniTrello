using MediatR;
using MiniTrello.Domain.Ticket;

namespace MiniTrello.Application.Ticket.Commands.UpdatePriority;

public sealed record UpdatePriorityCommand(Guid TicketId, Priority Priority) :IRequest;
