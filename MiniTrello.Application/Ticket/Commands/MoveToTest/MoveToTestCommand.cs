using MediatR;

namespace MiniTrello.Application.Ticket.Commands.MoveToTest;

public sealed record MoveToTestCommand(Guid TicketId) : IRequest;
