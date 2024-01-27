using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.Assign;

internal sealed class AssignCommandHandler : IRequestHandler<AssignCommand, Result>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;

    public AssignCommandHandler(IEventStore eventStore, IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<Result> Handle(AssignCommand request, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        var result = ticket.Assign(request.User);

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
