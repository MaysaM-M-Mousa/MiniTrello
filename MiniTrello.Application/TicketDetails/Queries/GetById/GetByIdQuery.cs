using MediatR;
using MiniTrello.Domain.Ticket.Projections.TicketDetails;

namespace MiniTrello.Application.TicketDetails.Queries.GetById;

public sealed record GetByIdQuery(Guid TicketId) : IRequest<TicketDetailsProjection?>;
