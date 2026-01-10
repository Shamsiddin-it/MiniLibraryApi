using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> Add(Book book)
        {
            return await bookService.Add(book);
        }

        [HttpPut]
        public async Task<Response<string>> Update(Book book)
        {
            return await bookService.Update(book);
        }

        [HttpDelete("{bookId}")]
        public async Task<Response<string>> Delete(int bookId)
        {
            return await bookService.Delete(bookId);
        }

        [HttpGet]
        public async Task<Response<List<Book>>> GetBooks()
        {
            return await bookService.GetBooks();   
        }

        [HttpGet("{bookId}")]
        public async Task<Response<Book>> GetBookById(int bookId)
        {
            return await bookService.GetBookById(bookId);
        }
    }
}
