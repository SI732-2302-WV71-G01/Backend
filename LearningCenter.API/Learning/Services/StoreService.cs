using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Exceptions;

namespace LearningCenter.API.Learning.Services;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StoreService(IStoreRepository storeRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _storeRepository = storeRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Store>> ListAsync()
    {
        return await _storeRepository.ListAsync();
    }

    public async Task<Store> GetByIdAsync(int id)
    {
        var store = await _storeRepository.FindByIdAsync(id);
        if (store == null) throw new KeyNotFoundException("Store not found");
        return store;
    }

    public async Task<IEnumerable<Store>> ListByUserIdAsync(int userId)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);

        if (existingUser == null)
            throw new AppException("Invalid User");
        
        return await _storeRepository.FindByUserIdAsync(userId);
    }

    public async Task<StoreResponse> SaveAsync(Store store)
    {
        var existingCategory = await _userRepository.FindByIdAsync(store.UserId);
        if (existingCategory == null)
            return new StoreResponse("Invalid User");
        
        try
        {
            await _storeRepository.AddAsync(store);
            await _unitOfWork.CompleteAsync();
            return new StoreResponse(store);
        }
        catch (Exception e)
        {
            return new StoreResponse($"An error occurred while saving the store: {e.Message}");
        }
    }

    public async Task<StoreResponse> UpdateAsync(int storeId, Store store)
    {
        var existingTutorial = await _storeRepository.FindByIdAsync(storeId);
        
        // Validate Tutorial
        if (existingTutorial == null)
            return new StoreResponse("Store not found.");
        // Validate CategoryId
        var existingCategory = await _userRepository.FindByIdAsync(store.UserId);
        if (existingCategory == null)
            return new StoreResponse("Invalid User");
        
        // Modify Fields
        existingTutorial.Name = store.Name;
        existingTutorial.Description = store.Description;
        existingTutorial.Address = store.Address;
        existingTutorial.Encoded64LogoImage = store.Encoded64LogoImage;
        existingTutorial.UserId = store.UserId;

        try
        {
            _storeRepository.Update(existingTutorial);
            await _unitOfWork.CompleteAsync();

            return new StoreResponse(existingTutorial);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new StoreResponse($"An error occurred while updating the store: {e.Message}");
        }
    }

    public async Task<StoreResponse> DeleteAsync(int storeId)
    {
        var existingStore = await _storeRepository.FindByIdAsync(storeId);
        
        if (existingStore == null)
            return new StoreResponse("Store not found.");
        
        try
        {
            _storeRepository.Remove(existingStore);
            await _unitOfWork.CompleteAsync();

            return new StoreResponse(existingStore);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new StoreResponse($"An error occurred while deleting the store: {e.Message}");
        }
    }
}