using MediatR;
using MiniTrello.Contracts.Common;
using MiniTrello.Contracts.TicketDetailsProjection;

namespace MiniTrello.Application.TicketDetails.Queries.GetAll;

public sealed record GetAllTicketProjectionsQuery : IRequest<ListResponse<TicketDetailsProjectionResponse>>;
