using Microsoft.EntityFrameworkCore;
using Search.Domain.Entities;

namespace Search.Infrastructure
{
    /// <summary>
    /// BookingDBContext used to communicate with all In memory DB tables
    /// </summary>
    public class BookingDBContext : DbContext
    {
        public BookingDBContext(DbContextOptions<BookingDBContext> options) : base(options) { }

        /// <summary>
        /// User entity for User table
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Flight entity for Flight table
        /// </summary>
        public DbSet<Flight> Flights { get; set; }

        /// <summary>
        /// Booking entity for Booking table
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }
    }
}
