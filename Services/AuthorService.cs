using System.Net;
using Dapper;
using Microsoft.AspNetCore.Components.Authorization;
using WebApi.DTOs;

public class AuthorService(ApplicationDbContext applicationDbContext) : IAuthorService
{
    private readonly ApplicationDbContext dbContext = applicationDbContext;
    public async Task<Response<string>> Add(AuthorDto author1)
    {
        try
        {
            Author author = new Author
            {
                FullName = author1.FullName,
                BirthDate = author1.BirthDate,
                Country = author1.Country
            };
            using var conn = dbContext.Connection();
            var query = "insert into authors(fullname, birthdate, country) values(@fullname, @birthdate, @country)";
            var res = await conn.ExecuteAsync(query, new {fullname = author.FullName, birthdate = author.BirthDate, country = author.Country});
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

    public async Task<Response<string>> Delete(int authorId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "delete from authors where id=@id";
            var res = await conn.ExecuteAsync(query, new {id=authorId});
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

    public async Task<Response<Author>> GetAuthorById(int authorId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from authors where id=@id";
            var res = await conn.QueryFirstOrDefaultAsync<Author>(query, new {id=authorId});
            return res==null
            ? new Response<Author>(HttpStatusCode.InternalServerError, "Not found!")
            : new Response<Author>(HttpStatusCode.OK, "Found successfully!", res);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<Author>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<List<Author>>> GetAuthors()
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from authors";
            var res = await conn.QueryAsync<Author>(query);
            return res==null
            ? new Response<List<Author>>(HttpStatusCode.InternalServerError, "Not found!")
            : new Response<List<Author>>(HttpStatusCode.OK, "Found successfully!", res.ToList());
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<List<Author>>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<string>> Update(Author author)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "update authors set fullname=@fullname, birthdate=@birthdate, country=@country where id=@id";
            var res = await conn.ExecuteAsync(query, new {fullname = author.FullName, birthdate = author.BirthDate, country = author.Country, id=author.Id});
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