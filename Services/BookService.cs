using System;
using System.Net;
using Dapper;
using WebApi.DTOs;
using WebApi.Interfaces;

namespace WebApi.Services;

public class BookService(ApplicationDbContext applicationDbContext, ILogger<BookService> logger): IBookService 
{
    private readonly ApplicationDbContext dbContext = applicationDbContext;
    private readonly ILogger<BookService> _logger = logger;
    public async Task<Response<string>> Add(BookDto bookDto)
    {
        try
        {
            _logger.LogInformation("Starting the process of adding new book.");
            Book book = new Book
            {
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                Genre = bookDto.Genre,
                AuhtorId = bookDto.AuhtorId
            };
            using var conn = dbContext.Connection();
            var query = "insert into books(title, publishedyear, genre, authorid) values(@title, @publishedyear, @genre, @authorid)";
            var res = await conn.ExecuteAsync(query, new {title = book.Title, publishedyear=book.PublishedYear, genre=book.Genre, authorid=book.AuhtorId});
            if (res == 0)
            {
                _logger.LogWarning("The process failed, book not added.");
                return new Response<string>(HttpStatusCode.InternalServerError, "Could not add!");
            }
            else
            {
                _logger.LogInformation("The process finished all right. Book added!");
                return new Response<string>(HttpStatusCode.OK, "Added successfully!"); 
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, "The process of adding book failed!");
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
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
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<BookDto>> GetBookById(int bookId)
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from books where id=@id";
            var res = await conn.QueryFirstOrDefaultAsync<Book>(query, new {id=bookId});
            if (res == null)
            {
                _logger.LogWarning("There is no book with this id");
                return new Response<BookDto>(HttpStatusCode.NotFound, "not found");
            }
            BookDto bookDto = new BookDto
            {
                Title = res.Title,
                Genre = res.Genre,
                PublishedYear = res.PublishedYear,
                AuhtorId = res.AuhtorId
            };
            return new Response<BookDto>(HttpStatusCode.OK, "the book with that id", bookDto);
        }
        catch(Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return new Response<BookDto>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<List<Book>>> GetBooks()
    {
        try
        {
            using var conn = dbContext.Connection();
            var query = "select * from books";
            var res = await conn.QueryAsync<Book>(query);
            if (res == null)
            {
                _logger.LogWarning("There is no books.");
                return new Response<List<Book>>(HttpStatusCode.InternalServerError, "Not found!");
            }
            else
            {
                return new Response<List<Book>>(HttpStatusCode.OK, "Found successfully!", res.ToList());
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return new Response<List<Book>>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }

    public async Task<Response<string>> Update(UpdateBookDto bookDto)
    {
        try
        {
            Book book = new Book()
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Genre = bookDto.Genre,
                PublishedYear = bookDto.PublishedYear,
                AuhtorId = bookDto.AuhtorId
            };
            using var conn = dbContext.Connection();
            var query = "update books set title=@title, publishedyear=@publishedyear, genre=@genre, authorid=@authorid where id=@id";
            var res = await conn.ExecuteAsync(query, new {title = book.Title, publishedyear=book.PublishedYear, genre=book.Genre, authorid=book.AuhtorId, id=book.Id});
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
