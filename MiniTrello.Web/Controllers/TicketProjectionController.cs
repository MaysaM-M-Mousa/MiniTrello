﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniTrello.Application.TicketDetails.Queries.GetAll;
using MiniTrello.Application.TicketDetails.Queries.GetById;
using MiniTrello.Contracts.Common;
using MiniTrello.Contracts.TicketDetailsProjection;

namespace MiniTrello.Web.Controllers
{
    /// <summary>
    /// Controller for ticket projections
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ticket-projections")]
    public class TicketProjectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public TicketProjectionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Gets all ticket projections
        /// </summary>
        [HttpGet]
        public async Task<ListResponse<TicketDetailsProjectionResponse>> GetTicketProjections()
        {
            return await _mediator.Send(new GetAllTicketProjectionsQuery());
        }

        /// <summary>
        /// Gets the read model projection for a given ticket Id
        /// </summary>
        [HttpGet("{ticketId}")]
        public async Task<TicketDetailsProjectionResponse> GetTicketProjection(Guid ticketId)
        {
            return await _mediator.Send(new GetByIdQuery(ticketId));
        }
    }
}
