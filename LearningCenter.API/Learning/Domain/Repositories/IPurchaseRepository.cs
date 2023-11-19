using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> ListAsync();
    Task<Purchase> FindByIdAsync(int purchaseId);
    Task<IEnumerable<Purchase>> FindByUserIdAsync(int userId);
    Task AddAsync(Purchase purchase);
    void AddProductToPurchase(Purchase purchase, Product product);
    void Remove(Purchase purchase);
}