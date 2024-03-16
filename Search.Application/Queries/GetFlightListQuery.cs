using MediatR;
using Search.Domain.Entities;

namespace Search.Application.Queries
{
    public class GetFlightListQuery : IRequest<List<Flight>>
    {
    }
}
