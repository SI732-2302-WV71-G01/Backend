using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class StoreRepository : BaseRepository, IStoreRepository
{
    public StoreRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Store>> ListAsync()
    {
        return await _context.Stores.ToListAsync();
    }

    public async Task AddAsync(Store store)
    {
        await _context.Stores.AddAsync(store);
    }

    public async Task<Store> FindByIdAsync(int id)
    {
        return await _context.Stores.FindAsync(id);
    }

    public async Task<IEnumerable<Store>> FindByUserIdAsync(int userId)
    {
        return await _context.Stores
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Store store)
    {
        _context.Stores.Update(store);
    }

    public void Remove(Store store)
    {
        _context.Stores.Remove(store);
    }
}