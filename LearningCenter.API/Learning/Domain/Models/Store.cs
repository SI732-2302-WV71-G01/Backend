using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Models;

public class Store
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Encoded64LogoImage { get; set; }
    public IList<StoreImage> StoreImages { get; set; } = new List<StoreImage>(); 
    public IList<Product> Products { get; set; } = new List<Product>(); 
    public IList<Sale> Sales { get; set; } = new List<Sale>(); 
}