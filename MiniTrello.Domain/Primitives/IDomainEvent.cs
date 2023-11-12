using MediatR;

namespace MiniTrello.Domain.Primitives;

public interface IDomainEvent : INotification
{
    Guid Id { get; set; }

    Guid AggregateId { get; }

    DateTime OccurredAt { get; set; }
}
