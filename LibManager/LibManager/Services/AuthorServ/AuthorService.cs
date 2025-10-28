using LibManager.Models.DTOs;
using LibManager.Models.Entities;
using LibManager.Repository.AuthorRep;

namespace LibManager.Services.AuthorServ;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository authorRep;

    public AuthorService(IAuthorRepository authorRepository)
    {
        authorRep = authorRepository;
    }

    public async Task<List<AuthorDto>> GetAuthors()
    {
        var authors = await authorRep.GetAuthors();

        return authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth,
        }).ToList();
    }

    public async Task<List<AuthorWithBooksDto>> GetAuthorsWithBooks()
    {
        var authors = await authorRep.GetAuthorsWithBooks();

        return authors.Select(a => new AuthorWithBooksDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth,
            Books = a.Books.Select(b => new BookDto
            {
                Id = b.Id,
                AuthorId = b.AuthorId,
                PublishedYear = b.PublishedYear,
                Title = b.Title
            }).ToList()
        }).ToList();
    }

    public async Task<AuthorDto> GetAuthorById(int id)
    {
        var author = await authorRep.GetAuthorById(id);

        if (author == null)
            throw new Exception($"Author with ID {id} was not found.");

        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };
    }

    public async Task<List<AuthorDto>> GetAuthorsByName(string name)
    {
        var findAuthors = await authorRep.GetAuthorsByName(name);

        return findAuthors.Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth,
        }).ToList();
    }

    public async Task<AuthorDto> InsertAuthor(AuthorDto authorDto)
    {
        Author auth = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        var authorToReturn = await authorRep.InsertAuthor(auth);

        return new AuthorDto
        {
            Id = authorToReturn.Id,
            Name = authorToReturn.Name,
            DateOfBirth = authorToReturn.DateOfBirth,
        };
    }

    public async Task UpdateAuthor(int id, AuthorDto authorDto)
    {
        var authorToUpdate = await authorRep.GetAuthorById(id);
        if (authorToUpdate == null)
            throw new Exception($"Author with ID {id} was not found.");

        authorToUpdate.Name = authorDto.Name;
        authorToUpdate.DateOfBirth = authorDto.DateOfBirth;

        await authorRep.UpdateAuthor(authorToUpdate);
    }

    public async Task DeleteAuthor(int id)
    {
        var author = await authorRep.GetAuthorById(id);
        if (author == null)
            throw new Exception($"Author with ID {id} was not found.");

        await authorRep.DeleteAuthor(author);
    }
}