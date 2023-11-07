﻿using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.DomainEvents;

public sealed record TicketPriorityUpdatedDomainEvent(Guid AggregateId, Priority priority) : IDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
