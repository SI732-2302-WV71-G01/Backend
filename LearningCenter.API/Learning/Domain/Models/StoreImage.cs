namespace LearningCenter.API.Learning.Domain.Models;

public class StoreImage
{
    public int Id { get; set; }
    public string Enconded64Image { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
}