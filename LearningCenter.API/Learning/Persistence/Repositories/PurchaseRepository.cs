using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class PurchaseRepository : BaseRepository, IPurchaseRepository
{
    public PurchaseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Purchase>> ListAsync()
    {
        return await _context.Purchases
            .Include(p => p.User)
            .Include(p=>p.Products).ToListAsync();
    }

    public async Task<Purchase> FindByIdAsync(int purchaseId)
    {
        return await _context.Purchases
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == purchaseId);
    }

    public async Task<IEnumerable<Purchase>> FindByUserIdAsync(int userId)
    {
        return await _context.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.User).Include(p=>p.Products)
            .ToListAsync();
    }

    public async Task AddAsync(Purchase purchase)
    {
        await _context.Purchases.AddAsync(purchase);
    }

    public void AddProductToPurchase(Purchase purchase, Product product)
    {
        purchase.Products.Add(product);
    }

    public void Remove(Purchase purchase)
    {
        _context.Purchases.Remove(purchase);
    }
}