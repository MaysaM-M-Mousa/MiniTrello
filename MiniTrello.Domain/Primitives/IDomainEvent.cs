namespace MiniTrello.Domain.Primitives;

public interface IDomainEvent
{
    Guid Id { get; set; }

    DateTime OccurredAt { get; set; }
}
