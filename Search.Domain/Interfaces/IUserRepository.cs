using Search.Domain.Entities;

namespace Search.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> Create(User user);
    }
}
