using System;
using System.Net;
using Dapper;
using WebApi.Interfaces;

namespace WebApi.Services;

public class BookService(ApplicationDbContext applicationDbContext): IBookService 
{
    private readonly ApplicationDbContext dbContext = applicationDbContext;
    public async Task<Response<string>> Add(Book book)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "insert into books(title, publishedyear, genre, authorid) values(@title, @publishedyear, @genre, @authorid)";
            var res = await conn.ExecuteAsync(query, new {title = book.Title, publishedyear=book.PublishedYear, genre=book.Genre, authorid=book.AuhtorId});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not add!")
            : new Response<string>(HttpStatusCode.OK, "Added successfully!");
        }
        catch(Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong: {ex}");
        }
    }

    public async Task<Response<string>> Delete(int bookId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "delete from books where id=@id";
            var res = await conn.ExecuteAsync(query, new {id=bookId});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not delete!")
            : new Response<string>(HttpStatusCode.OK, "Deleted successfully!");
        }
        catch(Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong: {ex}");
        }
    }

    public async Task<Response<Book>> GetBookById(int bookId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from books where id=@id";
            var res = await conn.QueryFirstOrDefaultAsync<Book>(query, new {id=bookId});
            return res==null
            ? new Response<Book>(HttpStatusCode.InternalServerError, "Not found!")
            : new Response<Book>(HttpStatusCode.OK, "Found successfully!", res);
        }
        catch(Exception ex)
        {
            return new Response<Book>(HttpStatusCode.InternalServerError, $"Something went wrong: {ex}");
        }
    }

    public async Task<Response<List<Book>>> GetBooks()
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from books";
            var res = await conn.QueryAsync<Book>(query);
            return res==null
            ? new Response<List<Book>>(HttpStatusCode.InternalServerError, "Not found!")
            : new Response<List<Book>>(HttpStatusCode.OK, "Found successfully!", res.ToList());
        }
        catch(Exception ex)
        {
            return new Response<List<Book>>(HttpStatusCode.InternalServerError, $"Something went wrong: {ex}");
        }
    }

    public async Task<Response<string>> Update(Book book)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "update books set title=@title, publishedyear=@publishedyear, genre=@genre, authorid=@authorid where id=@id";
            var res = await conn.ExecuteAsync(query, new {title = book.Title, publishedyear=book.PublishedYear, genre=book.Genre, authorid=book.AuhtorId, id=book.Id});
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Could not update!")
            : new Response<string>(HttpStatusCode.OK, "Updated successfully!");
        }
        catch(Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong: {ex}");
        }
    }
}
