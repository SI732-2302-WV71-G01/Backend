using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Security.Authorization.Attributes;
using LearningCenter.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Learning.Controllers;
[AllowAnonymous]
[ApiController]
[Route("/api/v1/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    private readonly IMapper _mapper;

    public PurchasesController(IPurchaseService purchaseService, IMapper mapper)
    {
        _purchaseService = purchaseService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PurchaseResource>> GetAllAsync()
    {
        var purchases = await _purchaseService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseResource>>(purchases);

        return resources;

    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePurchaseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var purchase = _mapper.Map<SavePurchaseResource, Purchase>(resource);

        var purchaseResponse = await _purchaseService.SaveAsync(purchase); 

        if (!purchaseResponse.Success)
            return BadRequest(purchaseResponse.Message);

        var purchaseResource = _mapper.Map<Purchase, PurchaseResource>(purchaseResponse.Model);

        return Ok(purchaseResource);
    }
    
    [HttpPost("addProductToPurchase")]
    public async Task<IActionResult> AddProductToPurchase([FromBody] ProductToPurchaseResource form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var purchaseResponse = await _purchaseService.AddProductToPurchase(form.PurchaseId, form.ProductId);
        
        if (!purchaseResponse.Success)
            return BadRequest(purchaseResponse.Message);
        
        var purchaseResource = _mapper.Map<Purchase, PurchaseResource>(purchaseResponse.Model);
        
        return Ok(purchaseResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var purchaseResponse = await _purchaseService.DeleteAsync(id);
        
        if (!purchaseResponse.Success)
            return BadRequest(purchaseResponse.Message);

        var purchaseResource = _mapper.Map<Purchase, PurchaseResource>(purchaseResponse.Model);

        return Ok(purchaseResource);
    }
}