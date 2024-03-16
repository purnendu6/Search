namespace Search.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string BookingDetails { get; set; } = string.Empty;
    }
}
