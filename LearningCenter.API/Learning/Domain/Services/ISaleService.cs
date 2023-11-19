using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services;

public interface ISaleService
{
    Task<IEnumerable<Sale>> ListAsync();
    Task<SaleResponse> SaveAsync(Sale sale);
    Task<IEnumerable<Sale>> ListByStoreIdAsync(int storeId);
    Task<SaleResponse> AddProductToSale(int saleId, int productId);
    Task<SaleResponse> DeleteAsync(int saleId);
}