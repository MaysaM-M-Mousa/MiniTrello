using MediatR;
using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket.Projections.TicketDetails;

namespace MiniTrello.Application.TicketDetails.EventHandlers;

internal sealed class TicketDetailsProjectionHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly ITicketDetailsProjectionRepository _ticketDetailsProjectionRepository;

    public TicketDetailsProjectionHandler(ITicketDetailsProjectionRepository ticketDetailsProjectionRepository)
    {
        _ticketDetailsProjectionRepository = ticketDetailsProjectionRepository;
    }

    public async Task Handle(TDomainEvent @event, CancellationToken cancellationToken)
    {
        var projection = await _ticketDetailsProjectionRepository.GetProjectionByTicketId(@event.AggregateId)
            ?? new();

        projection.When(@event);

        await _ticketDetailsProjectionRepository.SaveProjection(projection);
    }
}