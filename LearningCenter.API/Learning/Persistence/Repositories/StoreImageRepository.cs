using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class StoreImageRepository : BaseRepository, IStoreImageRepository
{
    public StoreImageRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<StoreImage>> ListAsync()
    {
        return await _context.StoreImages
            .Include(p => p.Store)
            .ToListAsync();
    }

    public async Task<StoreImage> FindByIdAsync(int storeImageId)
    {
        return await _context.StoreImages
            .Include(p => p.Store)
            .FirstOrDefaultAsync(p => p.Id == storeImageId);
    }

    public async Task<IEnumerable<StoreImage>> FindByStoreIdAsync(int storeId)
    {
        return await _context.StoreImages
            .Where(p => p.StoreId == storeId)
            .Include(p => p.Store)
            .ToListAsync();
    }
    public async Task AddAsync(StoreImage storeImage)
    {
        await _context.StoreImages.AddAsync(storeImage);
    }

    public void Remove(StoreImage storeImage)
    {
        _context.StoreImages.Remove(storeImage);
    }
}