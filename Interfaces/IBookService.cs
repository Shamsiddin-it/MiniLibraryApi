using System;
using WebApi.DTOs;

namespace WebApi.Interfaces;

public interface IBookService
{
    Task<Response<string>> Add(BookDto book);
    Task<Response<string>> Update(Book book);
    Task<Response<string>> Delete(int bookId);
    Task<Response<List<Book>>> GetBooks();
    Task<Response<Book>> GetBookById(int bookId);
}
