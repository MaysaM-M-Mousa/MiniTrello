using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniTrello.Application.CommentProjection.Queries.GetById;
using MiniTrello.Contracts.CommentProjection;

namespace MiniTrello.Web.Controllers
{
    /// <summary>
    /// Controller for comment projection
    /// </summary>
    [Route("api/v{version:apiVersion}/comment-projections")]
    [ApiController]
    public class CommentProjectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public CommentProjectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a comment projection by comment id
        /// </summary>
        [HttpGet("{commentId}")]
        public async Task<CommentProjectionResponse> GetCommentProjection(Guid commentId)
        {
            return await _mediator.Send(new GetCommentProjectionByIdQuery(commentId));
        }
    }
}
