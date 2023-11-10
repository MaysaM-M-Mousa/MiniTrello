using MediatR;
using MiniTrello.Application.Common.Interfaces;

namespace MiniTrello.Application.Ticket.Commands.Assign;

internal sealed class AssignCommandHandler : IRequestHandler<AssignCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public AssignCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(AssignCommand request, CancellationToken cancellationToken)
    {
        var events = await _ticketRepository.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        ticket.Assign(request.User);

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        // publish domain events

        ticket.ClearUncommittedEvents();
    }
}
