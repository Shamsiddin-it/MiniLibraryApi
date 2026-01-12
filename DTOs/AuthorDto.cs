using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs;

public class AuthorDto
{
    [Required(ErrorMessage = "Eto pole obyazatelnoye!")]
    public string FullName {get; set;}=null!;
    public DateTime? BirthDate {get; set;}
    [Required]
    public string Country {get; set;}=null!;
}
