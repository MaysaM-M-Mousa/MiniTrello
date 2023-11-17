using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniTrello.Application.Ticket.Commands.Assign;
using MiniTrello.Application.Ticket.Commands.Create;
using MiniTrello.Application.Ticket.Commands.MoveToCodeReview;
using MiniTrello.Application.Ticket.Commands.MoveToDone;
using MiniTrello.Application.Ticket.Commands.MoveToInProgress;
using MiniTrello.Application.Ticket.Commands.MoveToTest;
using MiniTrello.Application.Ticket.Commands.Unassign;
using MiniTrello.Application.Ticket.Commands.UpdatePriority;
using MiniTrello.Application.Ticket.Commands.UpdateStoryPoints;
using MiniTrello.Contracts.Ticket;
using MiniTrello.Domain.Exceptions;

namespace MiniTrello.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task CreateTicket()
        {
            await _mediator.Send(new CreateCommand());
        }

        [HttpPost("{ticketId}/in-progress")]
        public async Task MoveToInProgress(Guid ticketId)
        {
            await _mediator.Send(new MoveToInProgressCommand(ticketId));
        }

        [HttpPost("{ticketId}/code-review")]
        public async Task MoveToCodeReview(Guid ticketId)
        {
            await _mediator.Send(new MoveToCodeReviewCommand(ticketId));
        }

        [HttpPost("{ticketId}/test")]
        public async Task MoveToTest(Guid ticketId)
        {
            await _mediator.Send(new MoveToTestCommand(ticketId));
        }

        [HttpPost("{ticketId}/done")]
        public async Task MoveToDone(Guid ticketId)
        {
            await _mediator.Send(new MoveToDoneCommand(ticketId));
        }

        [HttpPut("{ticketId}/priority")]
        public async Task UpdatePriority(Guid ticketId, [FromBody] UpdatePriorityRequest request)
        {
            if (ticketId != request.TicketId)
            {
                throw new MiniTrelloValidationException("Incompatible entity Ids");
            }

            await _mediator.Send(new UpdatePriorityCommand(request.TicketId, request.Priority));
        }

        [HttpPut("{ticketId}/story-points")]
        public async Task UpdateStoryPoints(Guid ticketId, [FromBody] UpdateStoryPointsRequest request)
        {
            if (ticketId != request.TicketId)
            {
                throw new MiniTrelloValidationException("Incompatible entity Ids");
            }

            await _mediator.Send(new UpdateStoryPointsCommand(request.TicketId, request.StoryPoints));
        }

        [HttpPut("{ticketId}/assignee")]
        public async Task UpdateAssignee(Guid ticketId, [FromBody] UpdateAssigneeRequest request)
        {
            if (ticketId != request.TicketId)
            {
                throw new MiniTrelloValidationException("Incompatible entity Ids");
            }

            await _mediator.Send(new AssignCommand(request.TicketId, request.User));
        }

        [HttpPost("{ticketId}/unassign")]
        public async Task UnassgineTicket(Guid ticketId)
        {
            await _mediator.Send(new UnassignCommand(ticketId));
        }
    }
}
