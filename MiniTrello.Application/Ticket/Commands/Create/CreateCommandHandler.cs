using MediatR;
using MiniTrello.Application.Common.Interfaces;

namespace MiniTrello.Application.Ticket.Commands.Create;

internal sealed class CreateCommandHandler : IRequestHandler<CreateCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public CreateCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var ticket = Domain.Ticket.Ticket.Create();

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        // publish domain events

        ticket.ClearUncommittedEvents();
    }
}
