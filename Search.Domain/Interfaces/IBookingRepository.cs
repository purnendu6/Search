using Search.Domain.Entities;

namespace Search.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAll();
        Task<int> Create(Booking booking);
    }
}
