using MediatR;
using MiniTrello.Domain.Ticket;
using MiniTrello.Domain.Ticket.Projections.TicketDetails;

namespace MiniTrello.Application.TicketDetails.EventHandlers;

internal sealed class TicketDetailsProjectionHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : ITicketDomainEvent
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