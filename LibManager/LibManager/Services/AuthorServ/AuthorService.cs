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

    public List<AuthorDto> GetAuthors()
    {
        return authorRep.GetAuthors().Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth,
        }).ToList();
    }

    public List<AuthorWithBooksDto> GetAuthorsWithBooks()
    {
        return authorRep.GetAuthorsWithBooks().Select(a => new AuthorWithBooksDto
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

    public AuthorDto GetAuthorById(int id)
    {
        var author = authorRep.GetAuthorById(id);

        if (author == null)
            throw new Exception($"Author with ID {id} was not found.");

        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };
    }

    public List<AuthorDto> GetAuthorsByName(string name)
    {
        return authorRep.GetAuthorsByName(name).Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth,
        }).ToList();
    }

    public AuthorDto InsertAuthor(AuthorDto authorDto)
    {
        if (string.IsNullOrEmpty(authorDto.Name))
            throw new Exception("Author name is required.");

        Author auth = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        var authorToReturn = authorRep.InsertAuthor(auth);

        return new AuthorDto
        {
            Id = authorToReturn.Id,
            Name = authorToReturn.Name,
            DateOfBirth = authorToReturn.DateOfBirth,
        };
    }

    public void UpdateAuthor(int id, AuthorDto authorDto)
    {
        if (string.IsNullOrEmpty(authorDto.Name))
            throw new Exception("Author name is required.");

        var authorToUpdate = authorRep.GetAuthorById(id);
        if (authorToUpdate == null)
            throw new Exception($"Author with ID {id} was not found.");

        authorToUpdate.Name = authorDto.Name;
        authorToUpdate.DateOfBirth = authorDto.DateOfBirth;

        authorRep.UpdateAuthor(authorToUpdate);
    }

    public void DeleteAuthor(int id)
    {
        var author = authorRep.GetAuthorById(id);
        if (author == null)
            throw new Exception($"Author with ID {id} was not found.");

        authorRep.DeleteAuthor(author);
    }
}