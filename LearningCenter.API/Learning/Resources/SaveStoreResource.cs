namespace LearningCenter.API.Learning.Resources;

public class SaveStoreResource
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Encoded64LogoImage { get; set; }
    public int UserId { get; set; }
}