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

    public async Task<List<Author>> GetAuthors()
    {
        return await libContext.Authors.ToListAsync();
    }

    public async Task<List<Author>> GetAuthorsWithBooks()
    {
        return await libContext.Authors.Include(a => a.Books).ToListAsync();
    }

    public async ValueTask<Author?> GetAuthorById(int id)
    {
        return await libContext.Authors.FindAsync(id);
    }

    public async Task<List<Author>> GetAuthorsByName(string name)
    {
        return await libContext.Authors.Where(a => a.Name.Contains(name)).ToListAsync();
    }

    public async Task<Author> InsertAuthor(Author author)
    {
        await libContext.Authors.AddAsync(author);
        await libContext.SaveChangesAsync();
        return author;
    }

    public async Task UpdateAuthor(Author authorToUpdate)
    {
        libContext.Authors.Update(authorToUpdate);
        await libContext.SaveChangesAsync();
    }

    public async Task DeleteAuthor(Author author)
    {
        libContext.Authors.Remove(author);
        await libContext.SaveChangesAsync();
    }
}