
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Book
{
    public int Id {get; set;}
    [Required, MaxLength(120)]
    public string Title {get; set;}=null!;
    public int PublishedYear {get; set;}
    public string Genre {get; set;}=null!;
    public int AuthorId {get; set;}
    [JsonIgnore]
    public Author? Author {get; set;}
}