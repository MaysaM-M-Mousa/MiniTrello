using MediatR;

namespace MiniTrello.Application.Ticket.Commands.Assign;

public sealed record AssignCommand(Guid TicketId, string User) : IRequest;
