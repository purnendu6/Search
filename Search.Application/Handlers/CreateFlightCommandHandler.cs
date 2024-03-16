using MediatR;
using Search.Application.Commands;
using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Application.Handlers
{
    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, int>
    {
        private readonly IFlightRepository _flightRepository;
        public CreateFlightCommandHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public Task<int> Handle(CreateFlightCommand createFlightCommand, CancellationToken cancellationToken)
        {
            var flight = new Flight();
            return _flightRepository.Create(flight);
        }
    }
}
