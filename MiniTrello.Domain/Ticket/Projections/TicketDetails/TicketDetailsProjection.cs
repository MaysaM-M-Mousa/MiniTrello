using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket.DomainEvents;

namespace MiniTrello.Domain.Ticket.Projections.TicketDetails;

public class TicketDetailsProjection : IProjection
{
    public Guid TicketId { get; set; }

    public string Assignee { get; set; }

    public Priority Priority { get; set; }

    public int StoryPoints { get; set; }

    public TicketStatus Status { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? StartedOnUtc { get; set; }

    public DateTime? CompletedOnUtc { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public void When(IDomainEvent @event)
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
            case TicketDeletedDomainEvent:
                Apply((TicketDeletedDomainEvent)@event);
                break;
            default:
                throw new Exception($"Unsupported Event type {@event.GetType().Name}");
        }
    }

    private void Apply(TicketCreatedDomainEvent @event)
    {
        TicketId = @event.AggregateId;
        Assignee = @event.Assignee;
        Priority = @event.Priority;
        Status = @event.Status;
        IsCompleted = false;
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
        StartedOnUtc = @event.OccurredAt;
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
        IsCompleted = true;
        CompletedOnUtc = @event.OccurredAt;
    }

    private void Apply(TicketDeletedDomainEvent @event)
    {
        IsDeleted = true;
        DeletedOnUtc = @event.OccurredAt;
    }
}
