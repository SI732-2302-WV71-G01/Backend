using System.Text.Json.Serialization;
using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Security.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int RoleId { get; set; }
    //Relationship
    public Role Role { get; set; }
    
    [JsonIgnore]
    public string PasswordHash { get; set; }
    public IList<Store> Stores { get; set; } = new List<Store>();
    public IList<Purchase> Purchases { get; set; } = new List<Purchase>();
    
    //articles 
    public IList<Article> ArticlesList { get; set; } = new List<Article>();
}