using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs;

public class UpdateUserDto
{
    [Required]
    public int Id {get; set;}
    public string FullName {get; set;}=null!;
    public string Email {get; set;} = null!;
}
