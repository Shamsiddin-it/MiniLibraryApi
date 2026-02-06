using System;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Interfaces;

public interface IBookService
{
    Task<Response<string>> Add(BookDto book);
    Task<Response<string>> Update(UpdateBookDto book);
    Task<Response<string>> Delete(int bookId);
    Task<PagedResult<Book>> GetBooks(BookFilter filter, PagedQuery pagedQuery);
    Task<Response<BookDto>> GetBookById(int bookId);
}
