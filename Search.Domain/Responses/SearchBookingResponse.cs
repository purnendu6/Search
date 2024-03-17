using Search.Domain.Dto;
using Search.Domain.Entities;

namespace Search.Domain.Responses
{
    public class SearchBookingResponse
    {
        public List<BookingDetails> Bookings { get; set; }
        public SortingOptions SortingOptions { get; set; }
        public PageInfo Pagination { get; set; }
    }
}
