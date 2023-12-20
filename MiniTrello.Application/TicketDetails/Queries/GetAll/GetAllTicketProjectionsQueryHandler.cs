using MediatR;
using MiniTrello.Contracts.Common;
using MiniTrello.Contracts.TicketDetailsProjection;
using MiniTrello.Domain.Ticket.Projections.TicketDetails;

namespace MiniTrello.Application.TicketDetails.Queries.GetAll;

internal sealed class GetAllTicketProjectionsQueryHandler : IRequestHandler<GetAllTicketProjectionsQuery, ListResponse<TicketDetailsProjectionResponse>>
{
    private readonly ITicketDetailsProjectionRepository _repository;

    public GetAllTicketProjectionsQueryHandler(ITicketDetailsProjectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListResponse<TicketDetailsProjectionResponse>> Handle(GetAllTicketProjectionsQuery request, CancellationToken cancellationToken)
    {
        var projections = await _repository.GetAll();

        var projectionReponse = projections
            .Select(p => new TicketDetailsProjectionResponse()
            {
                TicketId = p.TicketId,
                Assignee = p.Assignee,
                Priority = p.Priority.ToString(),
                StoryPoints = p.StoryPoints,
                Status = p.Status.ToString(),
                IsCompleted = p.IsCompleted,
                IsDeleted = p.IsDeleted,
                StartedOnUtc = p.StartedOnUtc,
                CompletedOnUtc = p.CompletedOnUtc,
                DeletedOnUtc = p.DeletedOnUtc,
            })
            .ToList();

        return new ListResponse<TicketDetailsProjectionResponse>() { Entities = projectionReponse };
    }
}
