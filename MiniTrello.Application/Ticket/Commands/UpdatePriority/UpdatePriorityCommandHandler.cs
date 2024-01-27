using MediatR;
using MiniTrello.Domain.Primitives.Result;
using MiniTrello.Domain.Ticket;

namespace MiniTrello.Application.Ticket.Commands.UpdatePriority;

internal sealed class UpdatePriorityCommandHandler : IRequestHandler<UpdatePriorityCommand, Result>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;

    public UpdatePriorityCommandHandler(IEventStore eventStore, IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        var priority = Enum.Parse<Priority>(request.Priority);

        var result = ticket.UpdatePriority(priority);

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
