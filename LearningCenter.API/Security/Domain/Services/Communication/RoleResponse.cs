using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Security.Domain.Services.Communication;

public class RoleResponse : BaseResponse<Role>
{
    public RoleResponse(string message) : base(message)
    {
    }

    public RoleResponse(Role model) : base(model)
    {
    }
}