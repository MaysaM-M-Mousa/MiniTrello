using MiniTrello.Domain.Primitives;

namespace MiniTrello.Domain.Ticket;

public class Ticket : AggregateRoot
{
    public string Assignee { get; private set; } = string.Empty;

    public Priority Priority { get; private set; } = Priority.Low;

    public TicketStatus Status { get; private set; } = TicketStatus.ToDo;

    public bool IsCompleted { get; private set; } = false;

    public List<Comment> Comments { get; private set; } = new();

    public Ticket(Guid aggregateId) : base(aggregateId)
    {
    }

    public override void When(IDomainEvent @event)
    {
        base.When(@event);
    }
}
