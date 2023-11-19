using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Learning.Resources;

public class PurchaseResource
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int UserId { get; set; }
    public IList<ProductResource> Products { get; set; } = new List<ProductResource>();
}