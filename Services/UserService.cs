using System;
using System.Net;
using Dapper;
using WebApi.Interfaces;

namespace WebApi.Services;

public class UserService(ApplicationDbContext applicationDbContext): IUserService
{
    private readonly ApplicationDbContext dbContext = applicationDbContext;
    public async Task<Response<string>> Add(User user)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "insert into users(fullname, email, registeredat) values(@fullname, @email, @registeredat)";
            var res = await conn.ExecuteAsync(query, new {fullname = user.FullName, email=user.Email, registeredat=user.RegisteredAt});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not add!")
            : new Response<string>(HttpStatusCode.OK, "Added successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<string>> Delete(int userId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "delete from users where id=@id";
            var res = await conn.ExecuteAsync(query, new {id=userId});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not delete!")
            : new Response<string>(HttpStatusCode.OK, "Deleted successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<User>> GetUserById(int userId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from users where id=@id";
            var res = await conn.QueryFirstOrDefaultAsync<User>(query, new {id=userId});
            return res==null
            ? new Response<User>(HttpStatusCode.InternalServerError, "Not found!")
            : new Response<User>(HttpStatusCode.OK, "Found successfully!", res);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<User>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<List<User>>> GetUsers()
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from users";
            var res = await conn.QueryAsync<User>(query);
            return res==null
            ? new Response<List<User>>(HttpStatusCode.InternalServerError, "Not found!")
            : new Response<List<User>>(HttpStatusCode.OK, "Found successfully!", res.ToList());
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<List<User>>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<string>> Update(User user)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "update users set fullname=@fullname, email=@email, registeredat=@registeredat where id=@id";
            var res = await conn.ExecuteAsync(query, new {fullname = user.FullName, email=user.Email, registeredat=user.RegisteredAt, id=user.Id});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not update!")
            : new Response<string>(HttpStatusCode.OK, "Updated successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }
}
