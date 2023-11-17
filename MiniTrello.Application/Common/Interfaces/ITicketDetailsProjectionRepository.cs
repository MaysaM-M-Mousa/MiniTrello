using MiniTrello.Domain.Ticket.Projections.TicketDetails;

namespace MiniTrello.Application.Common.Interfaces;

public interface ITicketDetailsProjectionRepository
{
    Task SaveProjection(TicketDetailsProjection projection);

    Task<TicketDetailsProjection?> GetProjectionByTicketId(Guid ticketId);

    Task<List<TicketDetailsProjection>> GetAll();
}
