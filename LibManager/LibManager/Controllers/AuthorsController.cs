using LibManager.Data;
using LibManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly LibraryContext libContext;

    public AuthorsController(LibraryContext libraryContext)
    {
        libContext = libraryContext;
    }

    [HttpGet("authors")]
    public ActionResult<IEnumerable<Author>> GetAllAuthors()
    {
        return libContext.Authors.ToList();
    }

    [HttpGet("authors with books")]
    public ActionResult<IEnumerable<Author>> GetAuthorsWithBooks()
    {
        return libContext.Authors.Include(a => a.Books).ToList();
    }

    [HttpGet("{name}")]
    public ActionResult<IEnumerable<Author>> SearchAuthorByName(string name)
    {
        return libContext.Authors.Where(a => a.Name.Contains(name)).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Author> GetAuthorById(int id)
    {
        var author = libContext.Authors.Find(id);

        if (author == null)
            return NotFound($"Author with ID {id} was not found.");

        return author;
    }

    [HttpPost]
    public IActionResult InsertAuthor(Author author)
    {
        if (string.IsNullOrEmpty(author.Name))
            return BadRequest("Author name is required.");

        libContext.Authors.Add(author);
        libContext.SaveChanges();

        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, Author updatedAuthor)
    {
        if (string.IsNullOrEmpty(updatedAuthor.Name))
            return BadRequest("Author name is required.");

        var author = libContext.Authors.Find(id);
        if (author == null)
            return NotFound($"Author with ID {id} was not found.");

        author.Name = updatedAuthor.Name;
        author.DateOfBirth = updatedAuthor.DateOfBirth;
        libContext.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        var author = libContext.Authors.Find(id);
        if (author == null)
            return NotFound($"Author with ID {id} was not found.");

        libContext.Authors.Remove(author);
        libContext.SaveChanges();

        return NoContent();
    }
}