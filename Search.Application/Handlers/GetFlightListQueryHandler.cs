using MediatR;
using Search.Application.Queries;
using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Application.Handlers
{
    public class GetFlightListQueryHandler : IRequestHandler<GetFlightListQuery, List<Flight>>
    {
        private readonly IFlightRepository _flightRepository;
        public GetFlightListQueryHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public Task<List<Flight>> Handle(GetFlightListQuery getFlightListQuery, CancellationToken cancellationToken)
        {
            return _flightRepository.GetAll();
        }
    }
}
