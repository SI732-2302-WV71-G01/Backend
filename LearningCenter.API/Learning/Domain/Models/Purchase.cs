using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Models;

public class Purchase
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int UserId { get; set; }
    //Relationship
    public User User { get; set; }
    public IList<Product> Products { get; set; } = new List<Product>();
}