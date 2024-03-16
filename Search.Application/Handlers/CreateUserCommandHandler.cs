using MediatR;
using Search.Application.Commands;
using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<int> Handle(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            var user = new User();
            return _userRepository.Create(user);
        }
    }
}
