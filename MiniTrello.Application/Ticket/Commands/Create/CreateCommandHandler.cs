using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.Create;

internal sealed class CreateCommandHandler : IRequestHandler<CreateCommand, Result>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMediator _mediator;

    public CreateCommandHandler(ITicketRepository ticketRepository, IMediator mediator)
    {
        _ticketRepository = ticketRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var result = Domain.Ticket.Ticket.Create();

        if (result.IsFailure)
        {
            return result;
        }

        var ticket = result.Value;

        await _ticketRepository.SaveEventsAsync(ticket.AggregateId, ticket.UncommittedEvents.ToList());

        foreach(var @event in ticket.UncommittedEvents.ToList())
        {
            await _mediator.Publish(@event);
        }

        ticket.ClearUncommittedEvents();

        return Result.Success();
    }
}
