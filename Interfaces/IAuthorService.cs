public interface IAuthorService
{
    Task<Response<string>> Add(Author author);
    Task<Response<string>> Update(Author author);
    Task<Response<string>> Delete(int authorId);
    Task<Response<List<Author>>> GetAuthors();
    Task<Response<Author>> GetAuthorById(int authorId);
}