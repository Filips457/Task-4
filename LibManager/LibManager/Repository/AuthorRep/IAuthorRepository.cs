using LibManager.Models.Entities;

namespace LibManager.Repository.AuthorRep;

public interface IAuthorRepository
{
    Task<List<Author>> GetAuthors();

    Task<List<Author>> GetAuthorsWithBooks();

    ValueTask<Author?> GetAuthorById(int id);

    Task<List<Author>> GetAuthorsByName(string name);

    Task<Author> InsertAuthor(Author author);

    Task UpdateAuthor(Author authorToUpdate);

    Task DeleteAuthor(Author author);
}