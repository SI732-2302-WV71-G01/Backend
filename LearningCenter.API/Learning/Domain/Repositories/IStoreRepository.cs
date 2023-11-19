using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface IStoreRepository 
{
    Task<IEnumerable<Store>> ListAsync();
    Task AddAsync(Store store);
    Task<Store> FindByIdAsync(int id);
    Task<IEnumerable<Store>> FindByUserIdAsync(int userId);
    void Update(Store store);
    void Remove(Store store);
}