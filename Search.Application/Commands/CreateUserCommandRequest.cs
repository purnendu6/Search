using MediatR;
using Search.Domain.Entities;

namespace Search.Application.Commands
{
    public class CreateUserCommandRequest : IRequest<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
