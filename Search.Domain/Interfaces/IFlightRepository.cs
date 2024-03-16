using Search.Domain.Entities;

namespace Search.Domain.Interfaces
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetAll();
        Task<int> Create(Flight flight);
    }
}
