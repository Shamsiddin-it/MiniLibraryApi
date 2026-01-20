using System;

namespace WebApi.Entities;

public class Profile
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public User? User {get; set;}
}
