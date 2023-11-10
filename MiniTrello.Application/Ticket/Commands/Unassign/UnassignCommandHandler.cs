using MediatR;
using MiniTrello.Application.Common.Interfaces;

namespace MiniTrello.Application.Ticket.Commands.Unassign;

internal sealed class UnassignCommandHandler : IRequestHandler<UnassignCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public UnassignCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(UnassignCommand request, CancellationToken cancellationToken)
    {
        var events = await _ticketRepository.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        ticket.Unassign();

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        // publish domain events

        ticket.ClearUncommittedEvents();
    }
}
