using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs;

public class AddBookLoanDto
{
    [Required]
    public int BookId {get; set;}
    [Required]
    public int UserId {get; set;}
}
