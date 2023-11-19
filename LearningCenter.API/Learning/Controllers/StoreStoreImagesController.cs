using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/stores/{storeId}/storeImages")]
public class StoreStoreImagesController : ControllerBase
{
    private readonly IStoreImageService _storeImageService;
    private readonly IMapper _mapper;

    public StoreStoreImagesController(IStoreImageService storeImageService, IMapper mapper)
    {
        _storeImageService = storeImageService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Store Images for given Store",
        Description = "Get existing Store Images associated with the specified Store",
        OperationId = "GetStoreStoreImages",
        Tags = new[] { "Stores"}
    )]
    public async Task<IEnumerable<StoreImageResource>> GetAllByStoreIdAsync(int storeId)
    {
        var storeImages = await _storeImageService.ListByStoreIdAsync(storeId);

        var resources = _mapper.Map<IEnumerable<StoreImage>, IEnumerable<StoreImageResource>>(storeImages);

        return resources;
    }
}