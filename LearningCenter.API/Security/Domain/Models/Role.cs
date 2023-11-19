namespace LearningCenter.API.Security.Domain.Models;

/*ROLES ID:
 1: USER
 2: ADMIN  
*/
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    //Relationship
    public IList<User> Users { get; set; } = new List<User>();
}