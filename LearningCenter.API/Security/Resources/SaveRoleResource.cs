using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Security.Resources;

public class SaveRoleResource
{
    [SwaggerSchema("Role Name")]
    [Required]
    public string Name { get; set; }
}