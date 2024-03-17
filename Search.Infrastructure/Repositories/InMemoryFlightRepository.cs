using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Infrastructure.Repositories
{
    public class InMemoryFlightRepository : IFlightRepository
    {
        private readonly BookingDBContext _dbContext;
        public InMemoryFlightRepository(BookingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Flight> Create(Flight flight)
        {
            _dbContext.Flights.Add(flight);
            _dbContext.SaveChanges();
            return Task.FromResult(flight);
        }

        public Task<List<Flight>> GetAll()
        {
            var flights = _dbContext.Flights.ToList();
            return Task.FromResult(flights);
        }
    }
}
