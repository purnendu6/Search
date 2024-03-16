using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        public Task<int> Create(User user)
        {
            _users.Add(user);
            return Task.FromResult(user.UserId);
        }
        public Task<List<User>> GetAll()
        {
            return Task.FromResult(_users);
        }
    }
}
