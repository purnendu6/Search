using MediatR;
using Microsoft.AspNetCore.Mvc;
using Search.Application.Commands;
using Search.Application.Queries;
using Search.Domain.Dto;
using Search.Domain.Entities;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/v1.0/booking")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Booking>))]
        public async Task<IActionResult> GetBookings()
        {
            var user = HttpContext.Items["User"] as string;
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                var result = await _mediator.Send(new GetBookingListQuery());
                return Ok(result);
            }
        }

        [HttpPost("/api/v1.0/booking/create")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult> PostBooking([FromBody] CreateBookingCommand createBookingCommand)
        {
            var result = await _mediator.Send(createBookingCommand);
            return Ok(result);
        }
    }
}
