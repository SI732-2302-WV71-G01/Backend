using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Security.Persistence.Repositories;

public class RoleRepository : BaseRepository, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Role>> ListAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role> FindByIdAsync(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task AddAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
    }
}