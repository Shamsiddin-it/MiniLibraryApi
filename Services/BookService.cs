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
                AuthorId = bookDto.AuhtorId
            };
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Added ok");
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
            var book = dbContext.Books.Find(bookId);
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "deleted");
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
            var res = dbContext.Books.Find(bookId);
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
                AuhtorId = res.AuthorId
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
            var res = dbContext.Books.ToList();
            if (res == null)
            {
                _logger.LogWarning("There is no books.");
                return new Response<List<Book>>(HttpStatusCode.InternalServerError, "Not found!");
            }
            else
            {
                return new Response<List<Book>>(HttpStatusCode.OK, "Found successfully!", res);
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
                AuthorId = bookDto.AuhtorId
            };
            var booku = dbContext.Books.Find(bookDto.Id);
            booku.Title = book.Title;
            booku.Genre = book.Genre;
            booku.PublishedYear = book.PublishedYear;
            booku.AuthorId = bookDto.AuhtorId;
            await dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Updated successfully!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, $"Something went wrong!");
        }
    }
}
