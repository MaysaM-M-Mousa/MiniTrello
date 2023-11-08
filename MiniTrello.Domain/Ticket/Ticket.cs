using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.Domain.Ticket;

public class Ticket : AggregateRoot
{
    public string Assignee { get; private set; }

    public TicketStatus Status { get; private set; }

    public bool IsCompleted { get; private set; }

    public List<Comment> Comments { get; private set; } = new();

    public Ticket(Guid aggregateId) : base(aggregateId)
    {
    }

    private Ticket(Guid aggregateId, string assignee, TicketStatus status) : base(aggregateId)
    {
        Assignee = assignee;
        Status = status;
    }

    public static Ticket Create()
    {
        var ticket = new Ticket(Guid.NewGuid(), string.Empty, TicketStatus.ToDo);

        var @event = new TicketCreatedDomainEvent(ticket.AggregateId, ticket.Assignee, Priority.None, ticket.Status);

        ticket.AddUncommittedEvent(@event);
        ticket.Apply(@event);

        return ticket;
    }

    public void Assign(string assignee)
    {
        if (string.IsNullOrEmpty(assignee))
        {
            throw new NullReferenceException(nameof(assignee));
        }

        var @event = new TicketAssignedDomainEvent(AggregateId, assignee);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void Unassign()
    {
        if (string.IsNullOrEmpty(Assignee))
        {
            throw new TicketAlreadyUnassignedException();
        }

        var @event = new TicketUnassignedDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void UpdatePriority(Priority priority)
    {
        var @event = new TicketPriorityUpdatedDomainEvent(AggregateId, priority);

        AddUncommittedEvent(@event);
    }

    public void UpdateStoryPoints(int storyPoints)
    {
        var @event = new TicketStoryPointsUpdatedDomainEvent(AggregateId, storyPoints);

        AddUncommittedEvent(@event);
    }

    public void MoveToInProgress()
    {
        if (Status == TicketStatus.InProgress)
        {
            return;
        }

        if (Status == TicketStatus.CodeReview || Status == TicketStatus.Done)
        {
            throw new MiniTrelloValidationException("Only Ticket in ToDo or Test lists can be moved to InProgress list!");
        }

        var @event = new TicketMovedToInProgressDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void MoveToCodeReview()
    {
        if (Status == TicketStatus.CodeReview)
        {
            return;
        }

        if (Status != TicketStatus.InProgress)
        {
            throw new MiniTrelloValidationException("Only InProgress tickets can be moved to CodeReview!");
        }

        var @event = new TicketMovedToCodeReviewDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void MoveToTest()
    {
        if (Status == TicketStatus.Test)
        {
            return;
        }

        if (Status != TicketStatus.CodeReview)
        {
            throw new MiniTrelloValidationException("Only CodeReview tickets can be moved to Test!");
        }

        var @event = new TicketMovedToTestDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void MoveToDone()
    {
        if (Status == TicketStatus.Done)
        {
            return;
        }

        if (Status != TicketStatus.Test)
        {
            throw new MiniTrelloValidationException("Only Test tickets can be moved to Done!");
        }

        var @event = new TicketMovedToDoneDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public static Ticket Load(Guid aggregateId, List<IDomainEvent> events)
    {
        var ticket = new Ticket(aggregateId);

        foreach(var @event in events)
        {
            ticket.When(@event);
        }

        return ticket;
    }

    public override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case TicketCreatedDomainEvent:
                Apply((TicketCreatedDomainEvent)@event);
                break;
            case TicketAssignedDomainEvent: 
                Apply((TicketAssignedDomainEvent)@event);
                break;
            case TicketUnassignedDomainEvent:
                Apply((TicketUnassignedDomainEvent)@event);
                break;
            case TicketMovedToInProgressDomainEvent:
                Apply((TicketMovedToInProgressDomainEvent)@event);
                break;
            case TicketMovedToCodeReviewDomainEvent:
                Apply((TicketMovedToCodeReviewDomainEvent)@event);
                break;
            case TicketMovedToTestDomainEvent:
                Apply((TicketMovedToTestDomainEvent)@event);
                break;
            case TicketMovedToDoneDomainEvent:
                Apply((TicketMovedToDoneDomainEvent)@event);
                break;
            case TicketPriorityUpdatedDomainEvent:
            case TicketStoryPointsUpdatedDomainEvent:
                break;
            default:
                throw new Exception($"Unsupported Event type { @event.GetType().Name }");
        }
    }

    private void Apply(TicketCreatedDomainEvent @event)
    {
        Assignee = @event.Assignee;
        Status = @event.Status;
    }

    private void Apply(TicketAssignedDomainEvent @event)
    {
        Assignee = @event.Assignee;
    }

    private void Apply(TicketUnassignedDomainEvent @event)
    {
        Assignee = string.Empty;
    }

    private void Apply(TicketMovedToInProgressDomainEvent @event)
    {
        Status = TicketStatus.InProgress;
    }

    private void Apply(TicketMovedToCodeReviewDomainEvent @event)
    {
        Status = TicketStatus.CodeReview;
    }

    private void Apply(TicketMovedToTestDomainEvent @event)
    {
        Status = TicketStatus.Test;
    }

    private void Apply(TicketMovedToDoneDomainEvent @event)
    {
        Status = TicketStatus.Done;
    }
}
