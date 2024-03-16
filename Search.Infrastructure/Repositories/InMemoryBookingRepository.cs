using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Infrastructure.Repositories
{
    public class InMemoryBookingRepository : IBookingRepository
    {
        private static readonly List<Booking> _bookings = new();
        public Task<int> Create(Booking booking)
        {
            _bookings.Add(booking);
            return Task.FromResult(booking.BookingId);
        }

        public Task<List<Booking>> GetAll()
        {
            return Task.FromResult(_bookings);
        }
    }
}
