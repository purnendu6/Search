using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Search.Application.Commands;
using Search.Application.Queries;
using Search.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        private readonly IMediator _mediator;
        public FlightController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates records in Flight table
        /// </summary>
        /// <param name="request">CreateFlightCommandRequest Obj</param>
        /// <returns></returns>

        [HttpPost("/api/v1.0/flight/create")]
        public async Task<IActionResult> CreateFlight([FromBody] CreateFlightCommandRequest request)
        {
            //if user is null than throw Unauthorized error 
            var user = HttpContext.Items["User"] as string;
            if (user == null)
            {
                return Unauthorized("Please provide valid authentication token.");
            }
            else
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
        }

        /// <summary>
        /// Get all flight details from Flight table
        /// </summary>
        /// <returns></returns>

        [HttpGet("/api/v1.0/flight/flights")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Flight>))]
        public async Task<IActionResult> GetFlights()
        {
            //if user is null than throw Unauthorized error 
            var user = HttpContext.Items["User"] as string;
            if (user == null)
            {
                return Unauthorized("Please provide valid authentication token.");
            }
            else
            {
                var result = await _mediator.Send(new GetFlightListQuery());
                return Ok(result);
            }
        }
    }
}
