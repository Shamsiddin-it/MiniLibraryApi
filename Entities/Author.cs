public class Author
{
    public int Id {get; set;}
    public string FullName {get; set;}=null!;
    public DateTime? BirthDate {get; set;}
    public string Country {get; set;}=null!;

    public List<Book> Books {get; set;} = new();
}