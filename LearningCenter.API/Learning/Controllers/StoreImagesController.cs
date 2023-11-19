using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class StoreImagesController : ControllerBase
{
    private readonly IStoreImageService _storeImageService;
    private readonly IMapper _mapper;

    public StoreImagesController(IStoreImageService storeImageService, IMapper mapper)
    {
        _storeImageService = storeImageService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<StoreImageResource>> GetAllAsync()
    {
        var storeImages = await _storeImageService.ListAsync();
        var resources = _mapper.Map<IEnumerable<StoreImage>, 
            IEnumerable<StoreImageResource>>(storeImages);

        return resources;

    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveStoreImageResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var storeImageModel = _mapper.Map<SaveStoreImageResource, StoreImage>(resource);

        var storeImageResponse = await _storeImageService.SaveAsync(storeImageModel); 

        if (!storeImageResponse.Success)
            return BadRequest(storeImageResponse.Message);

        var storeImageResource = _mapper.Map<StoreImage, StoreImageResource>(storeImageResponse.Model);

        return Ok(storeImageResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var storeImageResponse = await _storeImageService.DeleteAsync(id);
        
        if (!storeImageResponse.Success)
            return BadRequest(storeImageResponse.Message);

        var storeImageResource = _mapper.Map<StoreImage, StoreImageResource>(storeImageResponse.Model);

        return Ok(storeImageResource);
    }

    

}