using MediatR;
using Search.Domain.Entities;

namespace Search.Application.Queries
{
    public class GetUserListQuery : IRequest<List<User>>
    {
    }
}
