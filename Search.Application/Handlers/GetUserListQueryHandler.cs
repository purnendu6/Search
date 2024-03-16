using MediatR;
using Search.Application.Queries;
using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Application.Handlers
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<List<User>> Handle(GetUserListQuery getUserListQuery, CancellationToken cancellationToken)
        {
            return _userRepository.GetAll();
        }
    }
}
