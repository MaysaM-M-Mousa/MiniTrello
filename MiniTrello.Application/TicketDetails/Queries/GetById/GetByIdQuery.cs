using MediatR;
using MiniTrello.Contracts.TicketDetailsProjection;

namespace MiniTrello.Application.TicketDetails.Queries.GetById;

public sealed record GetByIdQuery(Guid TicketId) : IRequest<TicketDetailsProjectionResponse>;
