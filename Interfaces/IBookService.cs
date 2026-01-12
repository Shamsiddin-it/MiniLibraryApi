using System;
using WebApi.DTOs;

namespace WebApi.Interfaces;

public interface IBookService
{
    Task<Response<string>> Add(BookDto book);
    Task<Response<string>> Update(UpdateBookDto book);
    Task<Response<string>> Delete(int bookId);
    Task<Response<List<Book>>> GetBooks();
    Task<Response<BookDto>> GetBookById(int bookId);
}
