using LearningCenter.API.Learning.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Resources;

public class CategoryResource
{
    [SwaggerSchema("Category Identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Category Name")]
    public string Name { get; set; }
    public IList<TutorialResource> Tutorials { get; set; } = new List<TutorialResource>();
}