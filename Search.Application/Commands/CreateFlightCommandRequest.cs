using MediatR;
using Search.Domain.Entities;

namespace Search.Application.Commands
{
    public class CreateFlightCommandRequest : IRequest<Flight>
    {
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; } 
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
