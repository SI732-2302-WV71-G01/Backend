using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Learning.Domain.Services;

public interface IPurchaseService
{
    Task<IEnumerable<Purchase>> ListAsync();
    Task<PurchaseResponse> SaveAsync(Purchase purchase);
    Task<IEnumerable<Purchase>> ListByUserIdAsync(int userId);
    Task<PurchaseResponse> AddProductToPurchase(int purchaseId, int productId);
    Task<PurchaseResponse> DeleteAsync(int purchaseId);
}