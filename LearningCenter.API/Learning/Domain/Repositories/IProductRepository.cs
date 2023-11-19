using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ListAsync();
    Task<Product> FindByIdAsync(int productId);
    Task<IEnumerable<Product>> FindByStoreIdAsync(int storeId);
    Task AddAsync(Product product);
    void Update(Product product);
    void Remove(Product product);
}