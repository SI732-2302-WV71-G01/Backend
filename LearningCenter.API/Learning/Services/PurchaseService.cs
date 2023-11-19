using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Learning.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseService(IPurchaseRepository purchaseRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _purchaseRepository = purchaseRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Purchase>> ListAsync()
    {
        return await _purchaseRepository.ListAsync();
    }

    public async Task<PurchaseResponse> SaveAsync(Purchase purchase)
    {
        var existingUser = await _userRepository.FindByIdAsync(purchase.UserId);

        if (existingUser == null)
            return new PurchaseResponse("Invalid User");
        
        try
        {
            await _purchaseRepository.AddAsync(purchase);
            await _unitOfWork.CompleteAsync();
            return new PurchaseResponse(purchase);
        }
        catch (Exception e)
        {
            return new PurchaseResponse($"An error occurred while saving the tutorial: {e.Message}");
        }
    }

    public async Task<IEnumerable<Purchase>> ListByUserIdAsync(int userId)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);

        if (existingUser == null)
            throw new AppException("Invalid User");
        
        return await _purchaseRepository.FindByUserIdAsync(userId);
    }

    public async Task<PurchaseResponse> AddProductToPurchase(int purchaseId, int productId)
    {
        var existingPurchase = await _purchaseRepository.FindByIdAsync(purchaseId);
        var existingProduct = await _productRepository.FindByIdAsync(productId);

        if (existingPurchase == null)
            return new PurchaseResponse("Invalid purchase");
        if (existingProduct == null)
            return new PurchaseResponse("Invalid product");
        try
        {
            _purchaseRepository.AddProductToPurchase(existingPurchase, existingProduct);
            await _unitOfWork.CompleteAsync();
            return new PurchaseResponse(existingPurchase);
        }
        catch (Exception e)
        {
            return new PurchaseResponse($"An error occurred while adding the product to the purchase: {e.Message}");
        }
    }

    public async Task<PurchaseResponse> DeleteAsync(int purchaseId)
    {
        var existingPurchase = await _purchaseRepository.FindByIdAsync(purchaseId);
        
        if (existingPurchase == null)
            return new PurchaseResponse("Purchase not found.");
        
        try
        {
            _purchaseRepository.Remove(existingPurchase);
            await _unitOfWork.CompleteAsync();

            return new PurchaseResponse(existingPurchase);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new PurchaseResponse($"An error occurred while deleting the purchase: {e.Message}");
        }

    }
}