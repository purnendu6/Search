using MediatR;
using Search.Domain.Dto;

namespace Search.Application.Commands
{
    public class CreateBookingCommandRequest : IRequest
    {
        public List<BookingDetail> BookingDetails { get; set; }
    }
}
