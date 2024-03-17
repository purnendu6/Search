namespace Search.Domain.Dto
{
    public class BookingDetail
    {
        public int FlightId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public string SeatNo { get; set; }
        public string Status { get; set; }
    }

    public class BookingDetails
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public string SeatNo { get; set; }
        public string Status { get; set; }
    }
}
