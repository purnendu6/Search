using MediatR;
using Search.Domain.Requests;
using Search.Domain.Responses;

namespace Search.Application.Queries
{
    public class SearchBookingQuery : IRequest<SearchBookingResponse>
    {
        public SearchBookingRequest SearchBookingRequest {  get; set; }
    }
}
