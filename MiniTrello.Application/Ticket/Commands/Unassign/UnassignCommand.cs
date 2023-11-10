using MediatR;

namespace MiniTrello.Application.Ticket.Commands.Unassign;

public sealed record UnassignCommand(Guid TicketId) : IRequest;
