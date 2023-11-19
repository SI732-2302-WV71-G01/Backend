using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Security.Exceptions;

namespace LearningCenter.API.Learning.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStoreRepository _storeRepository;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IStoreRepository storeRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _storeRepository = storeRepository;
    }
    
    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _productRepository.ListAsync();
    }

    public async Task<IEnumerable<Product>> ListByStoreIdAsync(int storeId)
    {
        var existingStore = await _storeRepository.FindByIdAsync(storeId);

        if (existingStore == null)
            throw new AppException("Invalid Store Oe");
        
        return await _productRepository.FindByStoreIdAsync(storeId);
    }

    public async Task<ProductResponse> SaveAsync(Product product)
    {
        var existingStore = await _storeRepository.FindByIdAsync(product.StoreId);

        if (existingStore == null)
            return new ProductResponse("Invalid Store");
        
        try
        {
            await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return new ProductResponse(product);
        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred while saving the product: {e.Message}");
        }
    }

    public async Task<ProductResponse> UpdateAsync(int productId, Product product)
    {
        var existingProduct = await _productRepository.FindByIdAsync(productId);
        
        // Validate Product
        if (existingProduct == null)
            return new ProductResponse("Product not found.");
        // Validate StoreId
        var existingStore = await _storeRepository.FindByIdAsync(product.StoreId);
        if (existingStore == null)
            return new ProductResponse("Invalid Store");
        
        // Modify Fields
        existingProduct.Name = product.Name;
        existingProduct.Category = product.Category;
        existingProduct.Rating = product.Rating;
        existingProduct.Price = product.Price;
        existingProduct.InventoryStatus = product.InventoryStatus;
        existingProduct.Image = product.Image;
        

        try
        {
            _productRepository.Update(existingProduct);
            await _unitOfWork.CompleteAsync();

            return new ProductResponse(existingProduct);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ProductResponse($"An error occurred while updating the product: {e.Message}");
        }
    }

    public async Task<ProductResponse> DeleteAsync(int productId)
    {
        var existingProduct = await _productRepository.FindByIdAsync(productId);
        
        // Validate Tutorial

        if (existingProduct == null)
            return new ProductResponse("Product not found.");
        
        try
        {
            _productRepository.Remove(existingProduct);
            await _unitOfWork.CompleteAsync();
            return new ProductResponse(existingProduct);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ProductResponse($"An error occurred while deleting the product: {e.Message}");
        }
    }
}