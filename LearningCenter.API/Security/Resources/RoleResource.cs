using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Security.Resources;

public class RoleResource
{
    [SwaggerSchema("Role Identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Role Name")]
    public string Name { get; set; }
}