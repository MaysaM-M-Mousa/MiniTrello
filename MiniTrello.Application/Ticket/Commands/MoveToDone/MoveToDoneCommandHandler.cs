using MediatR;
using MiniTrello.Application.Common.Interfaces;

namespace MiniTrello.Application.Ticket.Commands.MoveToDone;

internal sealed class MoveToDoneCommandHandler : IRequestHandler<MoveToDoneCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public MoveToDoneCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(MoveToDoneCommand request, CancellationToken cancellationToken)
    {
        var events = await _ticketRepository.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        ticket.MoveToDone();

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        // publish domain events

        ticket.ClearUncommittedEvents();
    }
}
