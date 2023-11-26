namespace MiniTrello.Domain.Ticket.Projections.TicketDetails;

public interface ITicketDetailsProjectionRepository
{
    Task SaveProjection(TicketDetailsProjection projection);

    Task<TicketDetailsProjection?> GetProjectionByTicketId(Guid ticketId);

    Task<List<TicketDetailsProjection>> GetAll();
}
