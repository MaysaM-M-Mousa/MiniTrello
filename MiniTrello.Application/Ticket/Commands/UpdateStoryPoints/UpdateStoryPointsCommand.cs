using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.UpdateStoryPoints;

public sealed record UpdateStoryPointsCommand(Guid TicketId, int StoryPoints) : IRequest<Result>;
