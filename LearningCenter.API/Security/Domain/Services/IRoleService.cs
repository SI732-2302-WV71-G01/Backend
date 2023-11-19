using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Security.Domain.Services.Communication;

namespace LearningCenter.API.Security.Domain.Services;

public interface IRoleService
{
    Task<IEnumerable<Role>> ListAsync();
    Task<RoleResponse> SaveAsync(Role role);
}