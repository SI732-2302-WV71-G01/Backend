using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Learning.Resources;

namespace LearningCenter.API.Learning.Domain.Services;

public interface IStoreImageService
{
    Task<IEnumerable<StoreImage>> ListAsync();
    Task<IEnumerable<StoreImage>> ListByStoreIdAsync(int storeId);
    Task<StoreImageResponse> SaveAsync(StoreImage storeImage);
    Task<StoreImageResponse> DeleteAsync(int storeImageId);
}