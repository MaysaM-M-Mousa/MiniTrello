using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Comment.ModifyComment;

internal sealed class ModifyCommentCommandHandler : IRequestHandler<ModifyCommentCommand, Result>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;

    public ModifyCommentCommandHandler(
        IEventStore eventStore,
        IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<Result> Handle(ModifyCommentCommand request, CancellationToken cancellationToken)
    {
        var commentEvents = await _eventStore.GetEventsAsync(request.CommentId);

        var comment = Domain.Ticket.Comment.Comment.Load(request.CommentId, commentEvents);

        var result = comment.ModifyContent(request.content);

        if (result.IsFailure)
        {
            return result;
        }

        await _eventStore.SaveEventsAsync(comment.AggregateId, comment.UncommittedEvents.ToList());
        
        foreach(var @event in comment.UncommittedEvents.ToList()) 
        {
            await _mediator.Publish(@event);
        }

        comment.ClearUncommittedEvents();

        return Result.Success();
    }
}
