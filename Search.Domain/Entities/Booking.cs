using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Search.Domain.Entities
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public int UserId { get; set; }

        public DateTime BookingDate { get; set; }
        public string SeatNo { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public Flight Flight { get; set; }

    }
}
