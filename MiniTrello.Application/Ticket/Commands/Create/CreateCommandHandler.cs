using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.Create;

internal sealed class CreateCommandHandler : IRequestHandler<CreateCommand, Result>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;

    public CreateCommandHandler(IEventStore eventStore, IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<Result> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var result = Domain.Ticket.Ticket.Create();

        if (result.IsFailure)
        {
            return result;
        }

        var ticket = result.Value;

        await _eventStore.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        foreach(var @event in ticket.UncommittedEvents.ToList())
        {
            await _mediator.Publish(@event);
        }

        ticket.ClearUncommittedEvents();

        return Result.Success();
    }
}
