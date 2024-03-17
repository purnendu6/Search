using Search.Domain.Dto;
using Search.Domain.Entities;
using Search.Domain.Requests;
using Search.Domain.Responses;

namespace Search.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAll();
        Task<List<BookingDetail>> Create(List<BookingDetail> bookingDetails);
        Task<SearchBookingResponse> GetSearchDetails(SearchBookingRequest searchBookingRequest);
    }
}
