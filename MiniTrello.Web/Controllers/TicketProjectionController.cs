using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniTrello.Application.TicketDetails.Queries.GetAll;
using MiniTrello.Application.TicketDetails.Queries.GetById;
using MiniTrello.Contracts.Common;
using MiniTrello.Contracts.TicketDetailsProjection;

namespace MiniTrello.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ticket-projections")]
    public class TicketProjectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketProjectionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ListResponse<TicketDetailsProjectionResponse>> GetTicketProjections()
        {
            return await _mediator.Send(new GetAllTicketProjectionsQuery());
        }

        [HttpGet("{ticketId}")]
        public async Task<TicketDetailsProjectionResponse> GetTicketProjection(Guid ticketId)
        {
            return await _mediator.Send(new GetByIdQuery(ticketId));
        }
    }
}
