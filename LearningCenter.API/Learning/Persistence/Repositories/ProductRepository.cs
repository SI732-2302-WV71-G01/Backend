using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class ProductRepository:  BaseRepository, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _context.Products
            .Include(p => p.Store)
            .ToListAsync();
    }

    public async Task<Product> FindByIdAsync(int productId)
    {
        return await _context.Products
            .Include(p => p.Store)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<IEnumerable<Product>> FindByStoreIdAsync(int storeId)
    {
        return await _context.Products
            .Where(p => p.StoreId == storeId)
            .Include(p => p.Store)
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}