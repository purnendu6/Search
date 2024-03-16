using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Infrastructure.Repositories
{
    public class InMemoryFlightRepository : IFlightRepository
    {
        private static readonly List<Flight> _flights = new();
        public Task<int> Create(Flight flight)
        {
            _flights.Add(flight);
            return Task.FromResult(flight.FlightId);
        }

        public Task<List<Flight>> GetAll()
        {
            return Task.FromResult(_flights);
        }
    }
}
