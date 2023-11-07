﻿using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket.DomainEvents;

public sealed record TicketStoryPointsUpdatedDomainEvent(Guid AggregateId, int StoryPoints) : IDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
}