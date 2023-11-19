using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Services;

public class StoreImageService : IStoreImageService
{
    private readonly IStoreImageRepository _storeImageRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StoreImageService(IStoreImageRepository storeImageRepository, IUnitOfWork unitOfWork, IStoreRepository storeRepository)
    {
        _storeImageRepository = storeImageRepository;
        _storeRepository = storeRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<StoreImage>> ListAsync()
    {
        return await _storeImageRepository.ListAsync();
    }

    public async Task<IEnumerable<StoreImage>> ListByStoreIdAsync(int storeId)
    {
        return await _storeImageRepository.FindByStoreIdAsync(storeId);
    }

    public async Task<StoreImageResponse> SaveAsync(StoreImage storeImage)
    {
        var existingStore = await _storeRepository.FindByIdAsync(storeImage.StoreId);
        if (existingStore == null)
            return new StoreImageResponse("Invalid Store");
        
        try
        {
            await _storeImageRepository.AddAsync(storeImage);
            await _unitOfWork.CompleteAsync();
            return new StoreImageResponse(storeImage);
        }
        catch (Exception e)
        {
            return new StoreImageResponse($"An error occurred while saving the store image: {e.Message}");
        }
    }

    public async Task<StoreImageResponse> DeleteAsync(int storeImageId)
    {
        var existingStoreImage = await _storeImageRepository.FindByIdAsync(storeImageId);
        
        if (existingStoreImage == null)
            return new StoreImageResponse("Store Image not found.");
        
        try
        {
            _storeImageRepository.Remove(existingStoreImage);
            await _unitOfWork.CompleteAsync();

            return new StoreImageResponse(existingStoreImage);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new StoreImageResponse($"An error occurred while deleting the Store Image: {e.Message}");
        }
    }
}