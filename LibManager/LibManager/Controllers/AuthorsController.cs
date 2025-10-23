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
        return Ok(libContext.Authors.ToList());
    }

    [HttpGet("authors-with-books")]
    public ActionResult<IEnumerable<Author>> GetAuthorsWithBooks()
    {
        return Ok(libContext.Authors.Include(a => a.Books).ToList());
    }

    [HttpGet("search-by/{name}")]
    public ActionResult<IEnumerable<Author>> SearchAuthorByName([FromRoute] string name)
    {
        return Ok(libContext.Authors.Where(a => a.Name.Contains(name)).ToList());
    }

    [HttpGet("search/{id}")]
    public ActionResult<Author> GetAuthorById([FromRoute] int id)
    {
        var author = libContext.Authors.Find(id);

        if (author == null)
            return NotFound($"Author with ID {id} was not found.");

        return Ok(author);
    }

    [HttpPost]
    public IActionResult InsertAuthor([FromBody] AuthorDto authorDto)
    {
        if (string.IsNullOrEmpty(authorDto.Name))
            return BadRequest("Author name is required.");

        var author = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth,
        };
        libContext.Authors.Add(author);
        libContext.SaveChanges();

        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor([FromRoute] int id, [FromBody] AuthorDto updatedAuthor)
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
    public IActionResult DeleteAuthor([FromRoute] int id)
    {
        var author = libContext.Authors.Find(id);
        if (author == null)
            return NotFound($"Author with ID {id} was not found.");

        libContext.Authors.Remove(author);
        libContext.SaveChanges();

        return NoContent();
    }
}