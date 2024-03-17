using MediatR;
using Search.Application.Commands;
using Search.Domain.Entities;
using Search.Domain.Interfaces;
using System.Security.Cryptography;

namespace Search.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, User>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<User> Handle(CreateUserCommandRequest createUserCommandRequest, CancellationToken cancellationToken)
        {
            var newUser = new User()
            {
                UserName = createUserCommandRequest.UserName,
                Email = createUserCommandRequest.Email,
                Password = GeneratePassword(createUserCommandRequest.Password),
                FirstName = createUserCommandRequest.FirstName,
                LastName = createUserCommandRequest.LastName,
                IsActive = createUserCommandRequest.IsActive
            };
            return _userRepository.Create(newUser);
        }

        private string GeneratePassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }
    }
}
