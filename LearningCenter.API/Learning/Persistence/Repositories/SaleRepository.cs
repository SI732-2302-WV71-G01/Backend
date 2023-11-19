using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class SaleRepository : BaseRepository, ISaleRepository
{
    public SaleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _context.Sales
            .Include(p => p.Store)
            .Include(s=>s.Products).ToListAsync();
    }

    public async Task<Sale> FindByIdAsync(int saleId)
    {
        return await _context.Sales
            .Include(p => p.Store)
            .FirstOrDefaultAsync(p => p.Id == saleId);
    }

    public async Task<IEnumerable<Sale>> FindByStoreIdAsync(int storeId)
    {
        return await _context.Sales
            .Where(p => p.StoreId == storeId)
            .Include(p => p.Store)
            .Include(s=>s.Products)
            .ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
    }

    public void AddProductToSale(Sale sale, Product product)
    {
        sale.Products.Add(product);
    }

    public void Remove(Sale sale)
    {
        _context.Sales.Remove(sale);
    }
}