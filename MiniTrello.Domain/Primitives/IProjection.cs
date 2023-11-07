namespace MiniTrello.Domain.Primitives;

public interface IProjection
{
    public void When(IDomainEvent @event);
}
