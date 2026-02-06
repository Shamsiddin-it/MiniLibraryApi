using System;

namespace WebApi.Filters;

public class BookFilter
{
    public string? Title {get; set;}=null!;
    public int PublishedYear {get; set;}
}
