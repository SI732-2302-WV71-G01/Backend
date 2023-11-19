using System.Net.Mime;
using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Security.Authorization.Attributes;
using LearningCenter.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Stores")]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;
    private readonly IMapper _mapper;
    
    
    public StoresController(IStoreService storeService, IMapper mapper)
    {
        _storeService = storeService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StoreResource>), 200)]
    public async Task<IEnumerable<StoreResource>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<Store>, 
            IEnumerable<StoreResource>>(await _storeService.ListAsync());
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var store = await _storeService.GetByIdAsync(id);
        var resource = _mapper.Map<Store, StoreResource>(store);
        return Ok(resource);
    }
    
    [AuthorizeAdmin]
    [HttpPost]
    [ProducesResponseType(typeof(StoreResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveStoreResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var storeModel = _mapper.Map<SaveStoreResource, Store>(resource);
        
        var storeResponse = await _storeService.SaveAsync(storeModel);
        
        if (!storeResponse.Success)
            return BadRequest(storeResponse.Message);
        
        var storeResource = _mapper.Map<Store, StoreResource>(storeResponse.Model);
        
        return Ok(storeResource);
    }
    
    [AuthorizeAdmin]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveStoreResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var store = _mapper.Map<SaveStoreResource, Store>(resource);

        var storeResponse = await _storeService.UpdateAsync(id, store); 

        if (!storeResponse.Success)
            return BadRequest(storeResponse.Message);

        var storeResource = _mapper.Map<Store, StoreResource>(storeResponse.Model);

        return Ok(storeResource);
    }
    
    [AuthorizeAdmin]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _storeService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var tutorialResource = _mapper.Map<Store, StoreResource>(result.Model);

        return Ok(tutorialResource);
    }

}