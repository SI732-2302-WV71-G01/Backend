using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Security.Authorization.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/purchases")]
public class UserPurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    private readonly IMapper _mapper;

    public UserPurchasesController(IPurchaseService purchaseService, IMapper mapper)
    {
        _purchaseService = purchaseService;
        _mapper = mapper;
    }
    
    [AuthorizeUser]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Purchases for given User",
        Description = "Get existing Purchases associated with the specified User",
        OperationId = "GetUserPurchases",
        Tags = new[] { "Users"}
    )]
    public async Task<IEnumerable<PurchaseResource>> GetAllByUserIdAsync(int userId)
    {
        
        var purchases = await _purchaseService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseResource>>(purchases);

        return resources;
    }
}