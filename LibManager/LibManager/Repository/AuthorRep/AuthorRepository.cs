using LibManager.Data;
using LibManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibManager.Repository.AuthorRep;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryContext libContext;

    public AuthorRepository(LibraryContext libraryContext)
    {
        libContext = libraryContext;
    }

    public List<Author> GetAuthors()
    {
        return libContext.Authors.ToList();
    }

    public List<Author> GetAuthorsWithBooks()
    {
        return libContext.Authors.Include(a => a.Books).ToList();
    }

    public Author? GetAuthorById(int id)
    {
        return libContext.Authors.Find(id);
    }

    public List<Author> GetAuthorsByName(string name)
    {
        return libContext.Authors.Where(a => a.Name.Contains(name)).ToList();
    }

    public Author InsertAuthor(Author author)
    {
        libContext.Authors.Add(author);
        libContext.SaveChanges();
        return author;
    }

    public void UpdateAuthor(Author authorToUpdate)
    {
        libContext.Authors.Update(authorToUpdate);
        libContext.SaveChanges();
    }

    public void DeleteAuthor(Author author)
    {
        libContext.Authors.Remove(author);
        libContext.SaveChanges();
    }
}