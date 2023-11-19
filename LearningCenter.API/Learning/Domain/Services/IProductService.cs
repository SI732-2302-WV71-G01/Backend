using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> ListAsync();
    Task<IEnumerable<Product>> ListByStoreIdAsync(int storeId);
    Task<ProductResponse> SaveAsync(Product product);
    Task<ProductResponse> UpdateAsync(int productId, Product product);
    Task<ProductResponse> DeleteAsync(int productId);
}