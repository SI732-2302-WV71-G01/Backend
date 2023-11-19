namespace LearningCenter.API.Learning.Resources;

public class SaleResource
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int PurchaserId { get; set; }
    public int StoreId { get; set; }
    public IList<ProductResource> Products { get; set; } = new List<ProductResource>();
}