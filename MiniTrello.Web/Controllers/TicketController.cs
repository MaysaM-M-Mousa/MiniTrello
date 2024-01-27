using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniTrello.Application.Ticket.Commands.AddComment;
using MiniTrello.Application.Ticket.Commands.Assign;
using MiniTrello.Application.Ticket.Commands.Create;
using MiniTrello.Application.Ticket.Commands.Delete;
using MiniTrello.Application.Ticket.Commands.MoveToCodeReview;
using MiniTrello.Application.Ticket.Commands.MoveToDone;
using MiniTrello.Application.Ticket.Commands.MoveToInProgress;
using MiniTrello.Application.Ticket.Commands.MoveToTest;
using MiniTrello.Application.Ticket.Commands.Unassign;
using MiniTrello.Application.Ticket.Commands.UpdatePriority;
using MiniTrello.Application.Ticket.Commands.UpdateStoryPoints;
using MiniTrello.Contracts.Comment;
using MiniTrello.Contracts.Ticket;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket()
        {
            var res = await _mediator.Send(new CreateCommand());

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPost("{ticketId}/in-progress")]
        public async Task<IActionResult> MoveToInProgress(Guid ticketId)
        {
            var res = await _mediator.Send(new MoveToInProgressCommand(ticketId));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPost("{ticketId}/code-review")]
        public async Task<IActionResult> MoveToCodeReview(Guid ticketId)
        {
            var res = await _mediator.Send(new MoveToCodeReviewCommand(ticketId));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPost("{ticketId}/test")]
        public async Task<IActionResult> MoveToTest(Guid ticketId)
        {
            var res = await _mediator.Send(new MoveToTestCommand(ticketId));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPost("{ticketId}/done")]
        public async Task<IActionResult> MoveToDone(Guid ticketId)
        {
            var res = await _mediator.Send(new MoveToDoneCommand(ticketId));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPut("{ticketId}/priority")]
        public async Task<IActionResult> UpdatePriority(Guid ticketId, [FromBody] UpdatePriorityRequest request)
        {
            var res = await _mediator.Send(new UpdatePriorityCommand(ticketId, request.Priority));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPut("{ticketId}/story-points")]
        public async Task<IActionResult> UpdateStoryPoints(Guid ticketId, [FromBody] UpdateStoryPointsRequest request)
        {
            var res = await _mediator.Send(new UpdateStoryPointsCommand(ticketId, request.StoryPoints));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPut("{ticketId}/assignee")]
        public async Task<IActionResult> UpdateAssignee(Guid ticketId, [FromBody] UpdateAssigneeRequest request)
        {
            var res = await _mediator.Send(new AssignCommand(ticketId, request.User));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPost("{ticketId}/unassign")]
        public async Task<IActionResult> UnassgineTicket(Guid ticketId)
        {
            var res = await _mediator.Send(new UnassignCommand(ticketId)); ;

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpDelete("{ticketId}")]
        public async Task<IActionResult> DeleteTicket(Guid ticketId)
        {
            var res = await _mediator.Send(new DeleteCommand(ticketId));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }

        [HttpPost("{ticketId}/comments")]
        public async Task<IActionResult> AddCommentToTicket(Guid ticketId, [FromBody] AddCommentRequest request)
        {
            var res = await _mediator.Send(new AddCommentCommand(ticketId, request.User, request.Content));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }
    }
}
