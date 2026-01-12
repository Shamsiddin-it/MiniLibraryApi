using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs;

public class BookDto
{
    [Required(ErrorMessage = "Hamin field hatmiyay, inro pur kun!")]
    public string Title {get; set;}=null!;
    public int PublishedYear {get; set;}
    public string Genre {get; set;}=null!;
    public int AuhtorId {get; set;}
}
