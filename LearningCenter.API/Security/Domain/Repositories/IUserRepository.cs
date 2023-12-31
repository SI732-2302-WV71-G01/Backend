﻿using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Security.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    Task<User> FindByUsernameAsync(string username);
    public bool ExistsByUsername(string username);
    public bool ExistsById(int id);
    User FindById(int id);
    void Update(User user);
    void Remove(User user);
}