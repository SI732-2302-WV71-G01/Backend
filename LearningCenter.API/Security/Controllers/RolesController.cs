using System.Net.Mime;
using AutoMapper;
using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Security.Domain.Services;
using LearningCenter.API.Security.Resources;
using LearningCenter.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Security.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Roles")] 
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService; 
    private readonly IMapper _mapper; 
     
    public RolesController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }
     
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _roleService.ListAsync(); 
        var resources = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleResource>>(roles);
        return Ok(resources); 
    }
     
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveRoleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages()); 

        var roleModel = _mapper.Map<SaveRoleResource, Role>(resource); 

        var roleResponse = await _roleService.SaveAsync(roleModel); 

        if (!roleResponse.Success)
            return BadRequest(roleResponse.Message); 

        var roleResource = _mapper.Map<Role, RoleResource>(roleResponse.Model); 

        return Created(nameof(PostAsync), roleResource); 
    }
}