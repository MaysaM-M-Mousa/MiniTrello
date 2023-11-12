using MediatR;
using MiniTrello.Application.Common.Interfaces;
using MiniTrello.Domain.Primitives;

namespace MiniTrello.Application.TicketDetails.EventHandlers;

internal sealed class TicketDetailsProjectionHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly ITicketDetailsProjectionRepository _ticketDetailsProjectionRepository;

    public TicketDetailsProjectionHandler(ITicketDetailsProjectionRepository ticketDetailsProjectionRepository)
    {
        _ticketDetailsProjectionRepository = ticketDetailsProjectionRepository;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        var projection = await _ticketDetailsProjectionRepository.GetProjectionByTicketId(notification.AggregateId)
            ?? new();

        projection.When(notification);

        await _ticketDetailsProjectionRepository.SaveProjection(projection);
    }
}