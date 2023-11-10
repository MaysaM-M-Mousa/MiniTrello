using MediatR;

namespace MiniTrello.Application.Ticket.Commands.MoveToInProgress;

public sealed record MoveToInProgressCommand(Guid TicketId) : IRequest;
