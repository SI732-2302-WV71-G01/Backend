using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Learning.Services;
using LearningCenter.API.Security.Authorization.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/stores")]
public class UserStoresController : ControllerBase
{
    private readonly IStoreService _storeService;
    private readonly IMapper _mapper;

    public UserStoresController(IStoreService storeService, IMapper mapper)
    {
        _storeService = storeService;
        _mapper = mapper;
    }
    
    //[AuthorizeAdmin]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Stores for given User",
        Description = "Get existing Stores associated with the specified User",
        OperationId = "GetUserStores",
        Tags = new[] { "Users"}
    )]
    public async Task<IEnumerable<StoreResource>> GetAllByUserIdAsync(int userId)
    {
        var tutorials = await _storeService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Store>, IEnumerable<StoreResource>>(tutorials);

        return resources;
    }
}