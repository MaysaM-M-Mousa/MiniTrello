using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Comment.DeleteComment;

internal sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;
    
    public DeleteCommentCommandHandler(
        IEventStore eventStore, 
        IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(request.CommentId);

        var comment = Domain.Ticket.Comment.Comment.Load(request.CommentId, events);

        var result = comment.DeleteComment();

        if (result.IsFailure)
        {
            return result;
        }

        await _eventStore.SaveEventsAsync(comment.AggregateId, comment.UncommittedEvents.ToList());

        foreach(var @event in comment.UncommittedEvents.ToList())
        {
            await _mediator.Publish(@event);
        }

        return Result.Success();
    }
}
