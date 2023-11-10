using MediatR;

namespace MiniTrello.Application.Ticket.Commands.UpdateStoryPoints;

public sealed record UpdateStoryPointsCommand(Guid TicketId, int StoryPoints) : IRequest;
