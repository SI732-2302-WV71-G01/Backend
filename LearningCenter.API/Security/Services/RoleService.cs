using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Domain.Services;
using LearningCenter.API.Security.Domain.Services.Communication;

namespace LearningCenter.API.Security.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository; 
    private readonly IUnitOfWork _unitOfWork; 
    
    public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Role>> ListAsync()
    {
        return await _roleRepository.ListAsync(); 
    }
    
    public async Task<RoleResponse> SaveAsync(Role role)
    {
        try
        {
            await _roleRepository.AddAsync(role); 
            await _unitOfWork.CompleteAsync(); 
            return new RoleResponse(role); 
        }
        catch (Exception e)
        {
            return new RoleResponse($"An error occurred while saving the role: {e.Message}"); 
        }
    }
}