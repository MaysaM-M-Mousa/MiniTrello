using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.MoveToInProgress;

internal sealed class MoveToInProgressCommandHandler : IRequestHandler<MoveToInProgressCommand, Result>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMediator _mediator;

    public MoveToInProgressCommandHandler(ITicketRepository ticketRepository, IMediator mediator)
    {
        _ticketRepository = ticketRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(MoveToInProgressCommand request, CancellationToken cancellationToken)
    {
        var events = await _ticketRepository.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);
        
        var result = ticket.MoveToInProgress();

        if (result.IsFailure)
        {
            return result;
        }

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        foreach (var @event in ticket.UncommittedEvents.ToList())
        {
            await _mediator.Publish(@event);
        }

        ticket.ClearUncommittedEvents();

        return Result.Success();
    }
}
