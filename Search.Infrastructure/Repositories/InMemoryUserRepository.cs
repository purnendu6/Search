using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly BookingDBContext _dbContext;
        public InMemoryUserRepository(BookingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<User> Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return Task.FromResult(user);
        }
        public Task<List<User>> GetAll()
        {
            var users = _dbContext.Users.ToList();
            return Task.FromResult(users);
        }
    }
}
