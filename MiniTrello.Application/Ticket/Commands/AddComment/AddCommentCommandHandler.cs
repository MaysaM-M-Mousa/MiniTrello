using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.AddComment;

public sealed class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;

    public AddCommentCommandHandler(IEventStore eventStore, IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        var result = ticket.AddComment(request.User, request.Content);

        if (result.IsFailure)
        {
            return result;
        }

        var addedComment = result.Value;

        await _eventStore.SaveEventsAsync(addedComment.AggregateId, addedComment.UncommittedEvents.ToList());

        foreach(var @event in addedComment.UncommittedEvents.ToList())
        {
            await _mediator.Publish(@event);
        }

        addedComment.ClearUncommittedEvents();

        return Result.Success();
    }
}
