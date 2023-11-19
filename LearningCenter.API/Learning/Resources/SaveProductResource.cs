namespace LearningCenter.API.Learning.Resources;

public class SaveProductResource
{
    public string Name { get; set; }
    public int Rating { get; set; }
    public string Image { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
    public string InventoryStatus { get; set; }
    public int StoreId { get; set; }
}