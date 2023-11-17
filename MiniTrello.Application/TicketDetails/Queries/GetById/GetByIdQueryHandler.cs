﻿using MediatR;
using MiniTrello.Application.Common.Interfaces;
using MiniTrello.Contracts.TicketDetailsProjection;

namespace MiniTrello.Application.TicketDetails.Queries.GetById;

internal sealed class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, TicketDetailsProjectionResponse>
{
    private readonly ITicketDetailsProjectionRepository _ticketDetailsProjectionRepository;

    public GetByIdQueryHandler(ITicketDetailsProjectionRepository ticketDetailsProjectionRepository)
    {
        _ticketDetailsProjectionRepository = ticketDetailsProjectionRepository;
    }

    public async Task<TicketDetailsProjectionResponse?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var tickerProjection = await _ticketDetailsProjectionRepository.GetProjectionByTicketId(request.TicketId);

        return new TicketDetailsProjectionResponse()
        {
            TicketId = tickerProjection.TicketId,
            Assignee = tickerProjection.Assignee,
            Priority = tickerProjection.Priority.ToString(),
            StoryPoints = tickerProjection.StoryPoints,
            Status = tickerProjection.Status.ToString(),
            IsCompleted = tickerProjection.IsCompleted,
            StartedOnUtc = tickerProjection.StartedOnUtc,
            CompletedOnUtc = tickerProjection.CompletedOnUtc,
        };
    }
}
