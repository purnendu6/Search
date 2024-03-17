using Microsoft.EntityFrameworkCore;
using Search.Domain.Dto;
using Search.Domain.Entities;
using Search.Domain.Interfaces;
using Search.Domain.Requests;
using Search.Domain.Responses;

namespace Search.Infrastructure.Repositories
{
    public class InMemoryBookingRepository : IBookingRepository
    {
        private readonly BookingDBContext _dbContext;
        public InMemoryBookingRepository(BookingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<BookingDetail>> Create(List<BookingDetail> bookingDetails)
        {
            var bookings = new List<Booking>();
            foreach (var item in bookingDetails)
            {
                var booking = new Booking();
                booking.BookingDate = item.BookingDate;
                booking.FlightId = item.FlightId;
                booking.SeatNo = item.SeatNo;
                booking.Status = item.Status;
                booking.UserId = item.UserId;
                bookings.Add(booking);
            }
            _dbContext.Bookings.AddRange(bookings);
            _dbContext.SaveChanges();
            return Task.FromResult(bookingDetails);
        }

        public Task<List<Booking>> GetAll()
        {
            var bookings = _dbContext.Bookings.ToList();
            return Task.FromResult(bookings);
        }

        public async Task<SearchBookingResponse> GetSearchDetails(SearchBookingRequest searchBookingRequest)
        {
            SearchBookingResponse searchBookingResponse = new();
            var query = await (from p in _dbContext.Bookings
                               select new BookingDetails()
                               {
                                   BookingId = p.BookingId,
                                   BookingDate = p.BookingDate,
                                   UserId = p.UserId,
                                   SeatNo = p.SeatNo,
                                   Status = p.Status,
                                   FlightId = p.FlightId
                               }).ToListAsync();

            // Apply filters
            if (!string.IsNullOrEmpty(searchBookingRequest.Status))
            {
                query = query.Where(p => p.Status.ToLower().Contains(searchBookingRequest.Status.ToLower())).ToList();
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(searchBookingRequest.SortingOptions.SortBy))
            {
                switch (searchBookingRequest.SortingOptions.SortBy.ToLower())
                {
                    case "seatno" when searchBookingRequest.SortingOptions.SortDescending is true:
                        query = query.OrderByDescending(p => p.SeatNo).ToList();
                        break;
                    case "status" when searchBookingRequest.SortingOptions.SortDescending is true:
                        query = query.OrderByDescending(p => p.Status).ToList();
                        break;
                    case "seatno" when searchBookingRequest.SortingOptions.SortDescending is false:
                        query = query.OrderBy(p => p.SeatNo).ToList();
                        break;
                    case "status" when searchBookingRequest.SortingOptions.SortDescending is false:
                        query = query.OrderBy(p => p.Status).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Apply pagination
            query = query.Skip((searchBookingRequest.Pagination.CurrentPage - 1) * searchBookingRequest.Pagination.PageSize)
                                  .Take(searchBookingRequest.Pagination.PageSize)
                                  .ToList();

            searchBookingResponse.Bookings = query;
            searchBookingResponse.Pagination = searchBookingRequest.Pagination;
            searchBookingResponse.SortingOptions = searchBookingRequest.SortingOptions;
            return searchBookingResponse;
        }
    }
}
