using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(IAuthorService authorService) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> Add(AuthorDto author)
        {
            return await authorService.Add(author);
        }

        [HttpPut("{authorId}")]
        public async Task<Response<string>> Update(int authorId, Author author)
        {
            return await authorService.Update(authorId, author);
        }

        [HttpDelete("{authorId}")]
        public async Task<Response<string>> Delete(int authorId)
        {
            return await authorService.Delete(authorId);
        }

        [HttpGet]
        public async Task<Response<List<Author>>> GetAuthors()
        {
            return await authorService.GetAuthors();   
        }

        [HttpGet("{authorId}")]
        public async Task<Response<Author>> GetAuthorById(int authorId)
        {
            return await authorService.GetAuthorById(authorId);
        }
    }
}
