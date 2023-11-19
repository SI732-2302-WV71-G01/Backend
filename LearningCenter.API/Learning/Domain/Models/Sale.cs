using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Models;

public class Sale
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int PurchaserId { get; set; } 
    
    public int StoreId { get; set; }
    public Store Store { get; set; }
    //Relationship
    public IList<Product> Products { get; set; } = new List<Product>();
}