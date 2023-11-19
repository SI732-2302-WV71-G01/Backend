using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class ArticleRepository : BaseRepository, IArticleRepository
{
    public ArticleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Article>> ListAsync()
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task AddAsync(Article article)
    {
        await _context.Articles.AddAsync(article);
    }

    public async Task<Article> FindByIdAsync(int id)
    {
        return await _context.Articles.FindAsync(id);
    }

    public async Task<IEnumerable<Article>> FindByUserIdAsync(int userId)
    {
        return await _context.Articles
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Article article)
    {
        _context.Articles.Update(article);
    }

    public void Remove(Article article)
    {
        _context.Articles.Remove(article);
    }
    
}