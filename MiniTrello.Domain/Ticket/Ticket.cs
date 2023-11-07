using MiniTrello.Domain.Exceptions;
using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.Domain.Ticket;

public class Ticket : AggregateRoot
{
    public string Assignee { get; private set; } = string.Empty;

    public Priority Priority { get; private set; } = Priority.Low;      // regular field

    public int StoryPoints { get; private set; }                        // regular field

    public TicketStatus Status { get; private set; } = TicketStatus.ToDo;

    public bool IsCompleted { get; private set; } = false;

    public List<Comment> Comments { get; private set; } = new();

    public Ticket(Guid aggregateId) : base(aggregateId)
    {
    }

    public void Assign(string assignee)
    {
        if (string.IsNullOrEmpty(assignee))
        {
            throw new NullReferenceException(nameof(assignee));
        }

        Assignee = assignee;

        var @event = new TicketAssignedDomainEvent(AggregateId, Assignee);

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
        Priority = priority;

        var @event = new TicketPriorityUpdatedDomainEvent(AggregateId, Priority);

        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void UpdateStoryPoints(int storyPoints)
    {
        StoryPoints = storyPoints;

        var @event = new TicketStoryPointsUpdatedDomainEvent(AggregateId, storyPoints);

        AddUncommittedEvent(@event);
        Apply(@event);
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

        Status = TicketStatus.InProgress;

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

        Status = TicketStatus.CodeReview;

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

        Status = TicketStatus.Test;

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

        Status = TicketStatus.Test;

        var @event = new TicketMovedToDoneDomainEvent(AggregateId);

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
            case TicketPriorityUpdatedDomainEvent:
                Apply((TicketPriorityUpdatedDomainEvent)@event);
                break;
            case TicketStoryPointsUpdatedDomainEvent:
                Apply((TicketStoryPointsUpdatedDomainEvent)@event);
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

    private void Apply(TicketPriorityUpdatedDomainEvent @event)
    {
        Priority = @event.priority;
    }

    private void Apply(TicketStoryPointsUpdatedDomainEvent @event)
    {
        StoryPoints = @event.StoryPoints;
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
