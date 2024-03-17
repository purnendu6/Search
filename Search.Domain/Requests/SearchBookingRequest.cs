using Search.Domain.Dto;

namespace Search.Domain.Requests
{
    public class SearchBookingRequest
    {
        public string Status { get; set; }
        public SortingOptions SortingOptions { get; set; }
        public PageInfo Pagination { get; set; }
    }
}
