using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Exceptions;

namespace LearningCenter.API.Learning.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ArticleService(IArticleRepository storeRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _articleRepository = storeRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Article>> ListAsync()
    {
        return await _articleRepository.ListAsync();
    }

    public async Task<Article> GetByIdAsync(int id)
    {
        var store = await _articleRepository.FindByIdAsync(id);
        if (store == null) throw new KeyNotFoundException("Article not found");
        return store;
    }

    public async Task<IEnumerable<Article>> ListByUserIdAsync(int userId)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);

        if (existingUser == null)
            throw new AppException("Invalid User");
        
        return await _articleRepository.FindByUserIdAsync(userId);
    }

    public async Task<ArticleResponse> SaveAsync(Article article)
    {
        var existingUser = await _userRepository.FindByIdAsync(article.UserId);
        if (existingUser == null)
            return new ArticleResponse("Invalid User");
        
        try
        {
            await _articleRepository.AddAsync(article);
            await _unitOfWork.CompleteAsync();
            return new ArticleResponse(article);
        }
        catch (Exception e)
        {
            return new ArticleResponse($"An error occurred while saving the article: {e.Message}");
        }
    }

    public async Task<ArticleResponse> UpdateAsync(int articleId, Article article)
    {
        var existingArticle = await _articleRepository.FindByIdAsync(articleId);
        
        if (existingArticle == null)
            return new ArticleResponse("Article not found.");
        // Validate UserId
        var existingCategory = await _userRepository.FindByIdAsync(article.UserId);
        if (existingCategory == null)
            return new ArticleResponse("Invalid User");
        
        // Modify Fields
        existingArticle.Name = article.Name;
        existingArticle.Description = article.Description;
        existingArticle.UserId = article.UserId;

        try
        {
            _articleRepository.Update(existingArticle);
            await _unitOfWork.CompleteAsync();

            return new ArticleResponse(existingArticle);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ArticleResponse($"An error occurred while updating the article: {e.Message}");
        }
    }

    public async Task<ArticleResponse> DeleteAsync(int articleId)
    {
        var existingArticle = await _articleRepository.FindByIdAsync(articleId);
        
        if (existingArticle == null)
            return new ArticleResponse("Article not found.");
        
        try
        {
            _articleRepository.Remove(existingArticle);
            await _unitOfWork.CompleteAsync();

            return new ArticleResponse(existingArticle);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ArticleResponse($"An error occurred while deleting the article: {e.Message}");
        }
    }
}