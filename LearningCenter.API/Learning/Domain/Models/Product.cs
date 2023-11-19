namespace LearningCenter.API.Learning.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public string Image { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
    public string InventoryStatus { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public IList<Purchase> Purchases { get; set; } = new List<Purchase>();
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}