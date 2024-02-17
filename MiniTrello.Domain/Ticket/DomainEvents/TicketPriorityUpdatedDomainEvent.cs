﻿using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.DomainEvents;

public sealed record TicketPriorityUpdatedDomainEvent(
    Guid AggregateId, 
    Priority priority) : ITicketDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    Guid IDomainEvent.AggregateId => AggregateId;

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
