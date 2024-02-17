using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniTrello.Application.Comment.AddComment;
using MiniTrello.Contracts.Comment;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Web.Controllers
{
    /// <summary>
    /// Controller for comments
    /// </summary>
    [Route("api/v{version:apiVersion}/comments")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adds a comment to a ticket
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddCommentToTicket(
            Guid ticketId, 
            [FromBody] AddCommentRequest request)
        {
            var res = await _mediator.Send(new AddCommentCommand(ticketId, request.User, request.Content));

            return res.Match<IActionResult>(
                onSuccess: () => Ok(),
                onFailure: (e) => BadRequest(e));
        }
    }
}
