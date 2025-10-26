using LibManager.Models.Entities;

namespace LibManager.Repository;

public interface IAuthorRepository
{
    List<Author> GetAuthors();

    List<Author> GetAuthorsWithBooks();

    Author? GetAuthorById(int id);

    List<Author> GetAuthorsByName(string name);

    Author InsertAuthor(Author author);

    void UpdateAuthor(Author authorToUpdate);

    void DeleteAuthor(Author author);
}