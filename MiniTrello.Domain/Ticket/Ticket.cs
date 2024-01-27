using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Primitives.Result;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.Domain.Ticket;

public sealed class Ticket : AggregateRoot
{
    public string Assignee { get; private set; }

    public TicketStatus Status { get; private set; }

    public bool IsDeleted { get; private set; }

    private Ticket() { }

    private Ticket(Guid aggregateId) : base(aggregateId) { }

    private Ticket(Guid aggregateId, string assignee, TicketStatus status) : base(aggregateId)
    {
        Assignee = assignee;
        Status = status;
    }

    public static Result<Ticket> Create()
    {
        var ticket = new Ticket(Guid.NewGuid(), string.Empty, TicketStatus.ToDo);

        var @event = new TicketCreatedDomainEvent(ticket.AggregateId, ticket.Assignee, Priority.None, ticket.Status);

        ticket.AddUncommittedEvent(@event);
        ticket.Apply(@event);

        return ticket;
    }

    public Result<Comment.Comment> AddComment(string user, string content)
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        return Comment.Comment.CreateComment(AggregateId, user, content);
    }

    public Result Assign(string assignee)
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        if (string.IsNullOrEmpty(assignee))
        {
            return TicketErrors.InvalidAssigneeName();
        }

        var @event = new TicketAssignedDomainEvent(AggregateId, assignee);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
    }

    public Result Unassign()
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        if (string.IsNullOrEmpty(Assignee))
        {
            return TicketErrors.UnassignedTicket();
        }

        var @event = new TicketUnassignedDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
    }

    public Result UpdatePriority(Priority priority)
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        var @event = new TicketPriorityUpdatedDomainEvent(AggregateId, priority);

        AddUncommittedEvent(@event);

        return Result.Success();
    }

    public Result UpdateStoryPoints(int storyPoints)
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        var @event = new TicketStoryPointsUpdatedDomainEvent(AggregateId, storyPoints);

        AddUncommittedEvent(@event);

        return Result.Success();
    }

    public Result MoveToInProgress()
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        if (Status == TicketStatus.InProgress)
        {
            return TicketErrors.AlreadyInProgress();
        }

        if (Status == TicketStatus.CodeReview || Status == TicketStatus.Done)
        {
            return TicketErrors.InvalidTicketOperation("Only Ticket in ToDo or Test lists can be moved to InProgress list!"); 
        }

        if (string.IsNullOrEmpty(Assignee))
        {
            return TicketErrors.UnassignedTicket("Can't move unassigned ticket to InProgress!");
        }

        var @event = new TicketMovedToInProgressDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
    }

    public Result MoveToCodeReview()
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        if (Status == TicketStatus.CodeReview)
        {
            return TicketErrors.AlreadyInCodeReview();
        }

        if (Status != TicketStatus.InProgress)
        {
            return TicketErrors.InvalidTicketOperation("Only tickets in progress can be moved to code review!");
        }

        var @event = new TicketMovedToCodeReviewDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
    }

    public Result MoveToTest()
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        if (Status == TicketStatus.Test)
        {
            return TicketErrors.AlreadyInTest();
        }

        if (Status != TicketStatus.CodeReview)
        {
            return TicketErrors.InvalidTicketOperation("Only CodeReview tickets can be moved to Test!");
        }

        var @event = new TicketMovedToTestDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
    }

    public Result MoveToDone()
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        if (Status == TicketStatus.Done)
        {
            return TicketErrors.AlreadyInDone();
        }

        if (Status != TicketStatus.Test)
        {
            return TicketErrors.InvalidTicketOperation("Only Test tickets can be moved to Done!");
        }

        var @event = new TicketMovedToDoneDomainEvent(AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
    }

    public Result Delete()
    {
        if (IsDeleted)
        {
            return TicketErrors.DeletedTicket();
        }

        var @event = new TicketDeletedDomainEvent(this.AggregateId);

        AddUncommittedEvent(@event);
        Apply(@event);

        return Result.Success();
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
            case TicketDeletedDomainEvent:
                Apply((TicketDeletedDomainEvent)@event);
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

    private void Apply(TicketDeletedDomainEvent @event)
    {
        IsDeleted = true;
    }
}
