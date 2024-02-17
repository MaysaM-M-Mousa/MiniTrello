using MediatR;
using MiniTrello.Domain.Ticket.Comment;
using MiniTrello.Domain.Ticket.Comment.Projections;

namespace MiniTrello.Application.CommentProjection.EventHandlers;

internal class CommentProjectionHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : ICommentDomainEvent
{
    private readonly ICommentProjectionRepository _commentProjectionRepository;

    public CommentProjectionHandler(ICommentProjectionRepository commentProjectionRepository)
    {
        _commentProjectionRepository = commentProjectionRepository;
    }

    public async Task Handle(TDomainEvent @event, CancellationToken cancellationToken)
    {
        var projection = await _commentProjectionRepository.GetProjectionById(@event.AggregateId)
            ?? new();

        projection.When(@event);

        await _commentProjectionRepository.SaveProjection(projection);
    }
}
