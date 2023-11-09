namespace MiniTrello.Domain.Primitives;

public abstract class AggregateRoot
{
    private IList<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();

    public Guid AggregateId { get; private set; }

    public virtual void When(IDomainEvent @event) { }

    protected AggregateRoot() { }

    public AggregateRoot(Guid aggregateId) => AggregateId = aggregateId;

    public IReadOnlyList<IDomainEvent> UncommittedEvents => _uncommittedEvents.ToList();

    public void AddUncommittedEvent(IDomainEvent domainEvent) => _uncommittedEvents.Add(domainEvent);

    public void ClearUncommittedEvents() => _uncommittedEvents.Clear();
}
