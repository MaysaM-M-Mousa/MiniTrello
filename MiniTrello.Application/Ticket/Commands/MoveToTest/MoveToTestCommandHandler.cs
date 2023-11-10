using MediatR;
using MiniTrello.Application.Common.Interfaces;

namespace MiniTrello.Application.Ticket.Commands.MoveToTest;

internal sealed class MoveToTestCommandHandler : IRequestHandler<MoveToTestCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public MoveToTestCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(MoveToTestCommand request, CancellationToken cancellationToken)
    {
        var events = await _ticketRepository.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        ticket.MoveToTest();

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        // publish domain events

        ticket.ClearUncommittedEvents();
    }
}
