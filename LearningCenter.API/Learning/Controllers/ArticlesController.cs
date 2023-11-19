using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Security.Authorization.Attributes;
using LearningCenter.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Learning.Controllers;
[ApiController]
[Route("/api/v1/[controller]")]
public class ArticlesController: ControllerBase
{
    
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public ArticlesController(IArticleService articleService, IMapper mapper)
    { 
        _articleService = articleService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<ArticleResource>> GetAllAsync()
    {
        var articles = await _articleService.ListAsync();
        var resource = _mapper.Map<IEnumerable<Article>,
            IEnumerable<ArticleResource>>(articles);
        return resource;
    }

    [AuthorizeAdmin]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveArticleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var article = _mapper.Map<SaveArticleResource, Article>(resource);
        var articleResponse = await _articleService.SaveAsync(article);

        if (!articleResponse.Success)
            return BadRequest((articleResponse.Message));
        
        var articleResource = _mapper.Map<Article,ArticleResource>(articleResponse.Model);

        return Ok(articleResource);
    }
    
    [AuthorizeAdmin]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveArticleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var articleModel = _mapper.Map<SaveArticleResource, Article>(resource);
        var articleResponse = await _articleService.UpdateAsync(id, articleModel);

        if (!articleResponse.Success)
            return BadRequest(articleResponse.Message);

        var articleResource = _mapper.Map<Article, ArticleResource>(articleResponse.Model);
        return Ok(articleResource);
    }
    
    [AuthorizeAdmin]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var articleResponse = await _articleService.DeleteAsync(id);
        if (!articleResponse.Success)
            return BadRequest(articleResponse.Message);

        var articleResource = _mapper.Map<Article, ArticleResource>(articleResponse.Model);

        return Ok(articleResource);
    }
    
}