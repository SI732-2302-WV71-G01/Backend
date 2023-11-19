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
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public SalesController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<SaleResource>> GetAllAsync()
    {
        var sales = await _saleService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);

        return resources;

    }
    

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveSaleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var sale = _mapper.Map<SaveSaleResource, Sale>(resource);

        var saleResponse = await _saleService.SaveAsync(sale); 

        if (!saleResponse.Success)
            return BadRequest(saleResponse.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(saleResponse.Model);

        return Ok(saleResource);
    }
    
    
    [HttpPost("addProductToSale")]
    public async Task<IActionResult> AddProductToSale([FromBody] ProductToSaleResource form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var saleResponse = await _saleService.AddProductToSale(form.SaleId, form.ProductId);
        
        if (!saleResponse.Success)
            return BadRequest(saleResponse.Message);
        
        var saleResource = _mapper.Map<Sale, SaleResource>(saleResponse.Model);
        
        return Ok(saleResource);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var saleResponse = await _saleService.DeleteAsync(id);
        
        if (!saleResponse.Success)
            return BadRequest(saleResponse.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(saleResponse.Model);

        return Ok(saleResource);
    }
}