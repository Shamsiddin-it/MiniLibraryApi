using System.Net;
using Dapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
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
            dbContext.Authors.Add(author);
            await dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Added Successfully!");
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
            var author = await dbContext.Authors.FindAsync(authorId);
            dbContext.Authors.RemoveRange(author);
            await dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "deleted successfully!");
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
            var res = dbContext.Authors.Include(a => a.Books).First(a=>a.Id==authorId);
            return new Response<Author>(HttpStatusCode.OK, "Found successfully!", res);
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
            var res = dbContext.Authors.Include(a=>a.Books).ToList();
            return new Response<List<Author>>(HttpStatusCode.OK, "Ok", res);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<List<Author>>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<string>> Update(int authorId, Author author)
    {
        try
        {
            var autor = await dbContext.Authors.FindAsync(authorId);
            autor.FullName = author.FullName;
            autor.BirthDate = author.BirthDate;
            autor.Country = author.Country;
            await dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "updated successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }
}