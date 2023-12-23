using MediatR;
using MiniTrello.Domain.Primitives.Result;
using MiniTrello.Domain.Ticket;

namespace MiniTrello.Application.Ticket.Commands.UpdatePriority;

internal sealed class UpdatePriorityCommandHandler : IRequestHandler<UpdatePriorityCommand, Result>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMediator _mediator;

    public UpdatePriorityCommandHandler(ITicketRepository ticketRepository, IMediator mediator)
    {
        _ticketRepository = ticketRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
    {
        var events = await _ticketRepository.GetEventsAsync(request.TicketId);

        var ticket = Domain.Ticket.Ticket.Load(request.TicketId, events);

        var priority = Enum.Parse<Priority>(request.Priority);

        var result = ticket.UpdatePriority(priority);

        if (result.IsFailure)
        {
            return result;
        }

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        foreach (var @event in ticket.UncommittedEvents.ToList())
        {
            await _mediator.Publish(@event);
        }

        ticket.ClearUncommittedEvents();

        return Result.Success();
    }
}
