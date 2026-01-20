using WebApi.Entities;

public class User
{
    public int Id {get; set;}
    public string FullName {get; set;}=null!;
    public string Email {get; set;} = null!;
    public DateTime? RegisteredAt{get; set;}=DateTime.UtcNow;
    public Profile? Profile {get; set;}
}