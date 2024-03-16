using MediatR;
using Search.Application.Queries;
using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Application.Handlers
{
    public class GetBookingListQueryHandler : IRequestHandler<GetBookingListQuery, List<Booking>>
    {
        private readonly IBookingRepository _bookingRepository;
        public GetBookingListQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public Task<List<Booking>> Handle(GetBookingListQuery getBookingListQuery, CancellationToken cancellationToken)
        {
            return _bookingRepository.GetAll();
        }
    }
}
