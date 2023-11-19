using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Exceptions;

namespace LearningCenter.API.Learning.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, 
        IStoreRepository storeRepository, IUserRepository userRepository)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _storeRepository = storeRepository;
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _saleRepository.ListAsync();
    }

    public async Task<SaleResponse> SaveAsync(Sale sale)
    {
        var existingUser = await _userRepository.FindByIdAsync(sale.PurchaserId);

        if (existingUser == null)
            return new SaleResponse("Invalid User");
        
        var existingStore = await _storeRepository.FindByIdAsync(sale.StoreId);

        if (existingStore == null)
            return new SaleResponse("Invalid Store");
        
        try
        {
            await _saleRepository.AddAsync(sale);
            await _unitOfWork.CompleteAsync();
            return new SaleResponse(sale);
        }
        catch (Exception e)
        {
            return new SaleResponse($"An error occurred while saving the sale: {e.Message}");
        }
    }

    public async Task<IEnumerable<Sale>> ListByStoreIdAsync(int storeId)
    {
        var existingStore = await _storeRepository.FindByIdAsync(storeId);

        if (existingStore == null)
            throw new AppException("Invalid Store");
        
        return await _saleRepository.FindByStoreIdAsync(storeId);
    }

    public async Task<SaleResponse> AddProductToSale(int saleId, int productId)
    {
        var existingSale = await _saleRepository.FindByIdAsync(saleId);
        var existingProduct = await _productRepository.FindByIdAsync(productId);

        if (existingSale == null)
            return new SaleResponse("Invalid sale");
        if (existingProduct == null)
            return new SaleResponse("Invalid product");
        try
        {
            _saleRepository.AddProductToSale(existingSale, existingProduct);
            await _unitOfWork.CompleteAsync();
            return new SaleResponse(existingSale);
        }
        catch (Exception e)
        {
            return new SaleResponse($"An error occurred while adding the product to the sale: {e.Message}");
        }
    }

    public async Task<SaleResponse> DeleteAsync(int saleId)
    {
        var existingSale = await _saleRepository.FindByIdAsync(saleId);
        
        if (existingSale == null)
            return new SaleResponse("Sale not found.");
        
        try
        {
            _saleRepository.Remove(existingSale);
            await _unitOfWork.CompleteAsync();

            return new SaleResponse(existingSale);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new SaleResponse($"An error occurred while deleting the sale: {e.Message}");
        }
    }
}