using MediatR;

namespace MiniTrello.Application.Ticket.Commands.MoveToCodeReview;

internal sealed class MoveToCodeReviewCommandHandler : IRequestHandler<MoveToCodeReviewCommand>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMediator _mediator;

    public MoveToCodeReviewCommandHandler(ITicketRepository ticketRepository, IMediator mediator)
    {
        _ticketRepository = ticketRepository;
        _mediator = mediator;
    }

    public async Task Handle(MoveToCodeReviewCommand request, CancellationToken cancellationToken)
    {
        var events = await _ticketRepository.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        ticket.MoveToCodeReview();

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        foreach (var @event in ticket.UncommittedEvents.ToList())
        {
            await _mediator.Publish(@event);
        }

        ticket.ClearUncommittedEvents();
    }
}
