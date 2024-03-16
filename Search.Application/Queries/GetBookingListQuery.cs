using MediatR;
using Search.Domain.Entities;

namespace Search.Application.Queries
{
    public class GetBookingListQuery : IRequest<List<Booking>>
    {
    }
}
