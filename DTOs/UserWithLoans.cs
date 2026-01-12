using System;

namespace WebApi.DTOs;

public class UserWithLoans
{
    public string FullName {get; set;}=null!;
    public string Email {get; set;}=null!;
    public List<BookLoan> BookLoans {get; set;}=[];
}
