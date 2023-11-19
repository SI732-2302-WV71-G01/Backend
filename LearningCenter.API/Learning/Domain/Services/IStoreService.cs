using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services;

public interface IStoreService
{
    Task<IEnumerable<Store>> ListAsync();
    Task<Store> GetByIdAsync(int id);
    Task<IEnumerable<Store>> ListByUserIdAsync(int userId);
    Task<StoreResponse> SaveAsync(Store store);
    Task<StoreResponse> UpdateAsync(int storeId, Store store);
    Task<StoreResponse> DeleteAsync(int storeId);
}