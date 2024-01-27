using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.MoveToDone;

internal sealed class MoveToDoneCommandHandler : IRequestHandler<MoveToDoneCommand, Result>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;

    public MoveToDoneCommandHandler(IEventStore eventStore, IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<Result> Handle(MoveToDoneCommand request, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        var result = ticket.MoveToDone();

        if (result.IsFailure)
        {
            return result;
        }

        await _eventStore.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        foreach (var @event in ticket.UncommittedEvents.ToList())
        {
            await _mediator.Publish(@event);
        }

        ticket.ClearUncommittedEvents();

        return Result.Success();
    }
}
