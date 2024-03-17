using MediatR;
using Search.Application.Queries;
using Search.Domain.Interfaces;
using Search.Domain.Responses;

namespace Search.Application.Handlers
{
    public class SearchBookingQueryHandler : IRequestHandler<SearchBookingQuery, SearchBookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        public SearchBookingQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public Task<SearchBookingResponse> Handle(SearchBookingQuery searchBookingQuery, CancellationToken cancellationToken)
        {
            return _bookingRepository.GetSearchDetails(searchBookingQuery.SearchBookingRequest);
        }
    }
}
