using MediatR;
using Search.Application.Commands;
using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Application.Handlers
{
    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommandRequest, Flight>
    {
        private readonly IFlightRepository _flightRepository;
        public CreateFlightCommandHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public Task<Flight> Handle(CreateFlightCommandRequest createFlightCommand, CancellationToken cancellationToken)
        {
            var flight = new Flight()
            {
                FlightNumber = createFlightCommand.FlightNumber,
                ArrivalAirport = createFlightCommand.ArrivalAirport,
                DepartureAirport = createFlightCommand.DepartureAirport,
                ArrivalTime = createFlightCommand.ArrivalTime,
                DepartureTime = createFlightCommand.DepartureTime,
                Price = createFlightCommand.Price
            };
            return _flightRepository.Create(flight);
        }
    }
}
