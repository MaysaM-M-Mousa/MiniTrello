using MediatR;
using MiniTrello.Application.Common.Interfaces;
using MiniTrello.Domain.Ticket.Projections.TicketDetails;

namespace MiniTrello.Application.TicketDetails.Queries.GetById;

internal sealed class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, TicketDetailsProjection?>
{
    private readonly ITicketDetailsProjectionRepository _ticketDetailsProjectionRepository;

    public GetByIdQueryHandler(ITicketDetailsProjectionRepository ticketDetailsProjectionRepository)
    {
        _ticketDetailsProjectionRepository = ticketDetailsProjectionRepository;
    }

    public async Task<TicketDetailsProjection?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _ticketDetailsProjectionRepository.GetProjectionByTicketId(request.TicketId);
    }
}
