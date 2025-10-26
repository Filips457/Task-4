using LibManager.Models.DTOs;

namespace LibManager.Services;

public interface IAuthorService
{
    List<AuthorDto> GetAuthors();

    List<AuthorWithBooksDto> GetAuthorsWithBooks();

    AuthorDto GetAuthorById(int id);

    List<AuthorDto>GetAuthorsByName(string name);

    AuthorDto InsertAuthor(AuthorDto authorDto);

    void UpdateAuthor(int id, AuthorDto authorDto);

    void DeleteAuthor(int id);
}