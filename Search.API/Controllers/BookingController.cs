using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Search.Application.Commands;
using Search.Application.Queries;
using Search.Domain.Requests;
using System;
using System.Threading.Tasks;

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

        /// <summary>
        /// Gets all booking details from Booking table
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1.0/booking")]
        public async Task<IActionResult> GetBookings()
        {
            //if user is null than throw Unauthorized error 
            var user = HttpContext.Items["User"] as string;
            if (user == null)
            {
                return Unauthorized("Please provide valid authentication token.");
            }
            else
            {
                var result = await _mediator.Send(new GetBookingListQuery());
                return Ok(result);
            }
        }

        /// <summary>
        /// Gets all records which matches the criteria in  Booking table
        /// </summary>
        /// <param name="searchBookingRequest"> SearchBookingRequest Object</param>
        /// <returns></returns>
        [HttpGet("/api/v1.0/booking/search")]
        public async Task<IActionResult> SearchBookings([FromQuery] SearchBookingRequest searchBookingRequest)
        {
            //if user is null than throw Unauthorized error 
            var user = HttpContext.Items["User"] as string;
            if (user == null)
            {
                return Unauthorized("Please provide valid authentication token.");
            }
            else
            {
                var result = await _mediator.Send(new SearchBookingQuery { SearchBookingRequest = searchBookingRequest });
                return Ok(result);
            }
        }

        /// <summary>
        /// Creates booking records in Booking table
        /// </summary>
        /// <param name="createBookingCommandRequest"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>

        [HttpPost("/api/v1.0/booking/create")]
        public async Task<ActionResult> PostBooking([FromBody] CreateBookingCommandRequest createBookingCommandRequest)
        {
            //if user is null than throw Unauthorized error 
            var user = HttpContext.Items["User"] as string;
            if (user == null)
            {
                throw new UnauthorizedAccessException("Please provide valid authentication token.");
            }
            else
            {
                await _mediator.Send(createBookingCommandRequest);
                return Ok("Data Inserted Successfully!");
            }
        }
    }
}
