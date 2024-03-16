﻿using Search.Domain.Entities;

namespace Search.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<int> Create(User user);
    }
}
