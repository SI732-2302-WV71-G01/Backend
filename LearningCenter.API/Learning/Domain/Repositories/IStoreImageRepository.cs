using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface IStoreImageRepository
{
    Task<IEnumerable<StoreImage>> ListAsync();
    Task<StoreImage> FindByIdAsync(int storeImageId);
    Task<IEnumerable<StoreImage>> FindByStoreIdAsync(int storeId);
    Task AddAsync(StoreImage storeImage);
    void Remove(StoreImage storeImage);
}