using LibManager.Models.DTOs;

namespace LibManager.Services.AuthorServ;

public interface IAuthorService
{
    Task<List<AuthorDto>> GetAuthors();

    Task<List<AuthorWithBooksDto>> GetAuthorsWithBooks();

    Task<AuthorDto> GetAuthorById(int id);

    Task<List<AuthorDto>> GetAuthorsByName(string name);

    Task<AuthorDto> InsertAuthor(AuthorDto authorDto);

    Task UpdateAuthor(int id, AuthorDto authorDto);

    Task DeleteAuthor(int id);
}