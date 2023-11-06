using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.Domain.Ticket;

public class Ticket : AggregateRoot
{
    public string Assignee { get; private set; } = string.Empty;

    public Priority Priority { get; private set; } = Priority.Low;

    public int StoryPoints { get; private set; }

    public TicketStatus Status { get; private set; } = TicketStatus.ToDo;

    public bool IsCompleted { get; private set; } = false;

    public List<Comment> Comments { get; private set; } = new();

    public Ticket(Guid aggregateId) : base(aggregateId)
    {
    }

    public void Assign(string assignee)
    {
        Assignee = assignee;

        var @event = new TicketAssignedDomainEvent(AggregateId, Assignee);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void Unassigne()
    {
        if (string.IsNullOrEmpty(Assignee))
        {
            throw new TicketAlreadyUnassignedException();
        }

        var @event = new TicketUnassignedDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case TicketAssignedDomainEvent: 
                Apply((TicketAssignedDomainEvent)@event);
                break;
            case TicketUnassignedDomainEvent:
                Apply((TicketUnassignedDomainEvent)@event);
                break;
            default:
                throw new Exception($"Unsupported Event type { @event.GetType().Name }");
        }
    }

    private void Apply(TicketAssignedDomainEvent @event)
    {
        Assignee = @event.Assignee;
    }

    private void Apply(TicketUnassignedDomainEvent @event)
    {
        Assignee = string.Empty;
    }
}
