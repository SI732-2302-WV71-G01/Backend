using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Security.Authorization.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/articles")]
[AllowAnonymous]
public class UserArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public UserArticlesController(IArticleService articleService, IMapper mapper)
    {
        _articleService = articleService;
        _mapper = mapper;
    }
    
    [AuthorizeAdmin]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Articles for given User",
        Description = "Get existing Articles associated with the specified User",
        OperationId = "GetUserArticles",
        Tags = new[] { "Users"}
    )]
    public async Task<IEnumerable<ArticleResource>> GetAllByUserIdAsync(int userId)
    {
        var tutorials = await _articleService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleResource>>(tutorials);

        return resources;
    }
}