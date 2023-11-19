using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> ListAsync();
    Task<Sale> FindByIdAsync(int saleId);
    Task<IEnumerable<Sale>> FindByStoreIdAsync(int storeId);
    Task AddAsync(Sale sale);
    void AddProductToSale(Sale sale, Product product);
    void Remove(Sale sale);
}