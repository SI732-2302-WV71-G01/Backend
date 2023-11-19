using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/store/{storeId}/sales")]
public class StoreSalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public StoreSalesController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Sales for given Store",
        Description = "Get existing Sales associated with the specified Store",
        OperationId = "GetStoreSales",
        Tags = new[] { "Stores"}
    )]
    public async Task<IEnumerable<SaleResource>> GetAllByStoreIdAsync(int storeId)
    {
        
        var sales = await _saleService.ListByStoreIdAsync(storeId);

        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);

        return resources;
    }
}