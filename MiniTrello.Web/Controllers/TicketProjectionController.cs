using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniTrello.Application.TicketDetails.Queries.GetById;
using MiniTrello.Contracts.TicketDetailsProjection;

namespace MiniTrello.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TicketProjectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketProjectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{ticketId}")]
        public async Task<TicketDetailsProjectionResponse> GetTicketProjection(Guid ticketId)
        {
            return await _mediator.Send(new GetByIdQuery(ticketId));
        }
    }
}
