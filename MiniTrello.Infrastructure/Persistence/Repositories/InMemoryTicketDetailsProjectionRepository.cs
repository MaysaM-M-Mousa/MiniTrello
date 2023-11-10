using MiniTrello.Application.Common.Interfaces;
using MiniTrello.Domain.Ticket.Projections.TicketDetails;

namespace MiniTrello.Infrastructure.Persistence.Repositories;

internal sealed class InMemoryTicketDetailsProjectionRepository : ITicketDetailsProjectionRepository
{
    private readonly List<TicketDetailsProjection> _projections = new();

    public Task<TicketDetailsProjection>? GetProjectionByTicketId(Guid ticketId)
    {
        var result = _projections
            .Where(p => p.TicketId == ticketId)
            .FirstOrDefault();

        return Task.FromResult(result);
    }

    public async Task SaveProjection(TicketDetailsProjection projection)
    {
        var dbProjection = await GetProjectionByTicketId(projection.TicketId);

        if (dbProjection is null)
        {
            _projections.Add(projection);
            return;
        }

        _projections.Remove(dbProjection);
        _projections.Add(projection);
    }
}
