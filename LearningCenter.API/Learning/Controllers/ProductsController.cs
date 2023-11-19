using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Security.Authorization.Attributes;
using LearningCenter.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ProductResource>> GetAllAsync()
    {
        var products = await _productService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

        return resources;

    }
    [AuthorizeAdmin]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var product = _mapper.Map<SaveProductResource, Product>(resource);

        var productResponse = await _productService.SaveAsync(product); 

        if (!productResponse.Success)
            return BadRequest(productResponse.Message);

        var tutorialResource = _mapper.Map<Product, ProductResource>(productResponse.Model);

        return Ok(tutorialResource);
    }
    [AuthorizeAdmin]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var product = _mapper.Map<SaveProductResource, Product>(resource);

        var productResponse = await _productService.UpdateAsync(id, product); //Devuelve ProductResponse

        if (!productResponse.Success)
            return BadRequest(productResponse.Message);

        var productResource = _mapper.Map<Product, ProductResource>(productResponse.Model);

        return Ok(productResource);
    }
    [AuthorizeAdmin]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var productResponse = await _productService.DeleteAsync(id);
        
        if (!productResponse.Success)
            return BadRequest(productResponse.Message);

        var productResource = _mapper.Map<Product, ProductResource>(productResponse.Model);

        return Ok(productResource);
    }
}