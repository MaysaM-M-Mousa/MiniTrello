using MediatR;
using MiniTrello.Application.Common.Interfaces;

namespace MiniTrello.Application.Ticket.Commands.UpdateStoryPoints;

internal sealed class UpdateStoryPointsCommandHandler : IRequestHandler<UpdateStoryPointsCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public UpdateStoryPointsCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(UpdateStoryPointsCommand request, CancellationToken cancellationToken)
    {
        var events = await _ticketRepository.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        ticket.UpdateStoryPoints(request.StoryPoints);

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        // publish domain events

        ticket.ClearUncommittedEvents();
    }
}
