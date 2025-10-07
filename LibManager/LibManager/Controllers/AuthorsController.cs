using LibManager.Data;
using LibManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LibManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Author>> GetAllAuthors()
    {
        return BadDataStorage.Authors;
    }

    [HttpGet("{id}")]
    public ActionResult<Author> GetAuthorById(int id)
    {
        var author = BadDataStorage.Authors.FirstOrDefault(a => a.Id == id);

        if (author == null)
            return NotFound($"Author with ID {id} was not found.");

        return author;
    }

    [HttpPost]
    public IActionResult InsertAuthor(Author author)
    {
        if (string.IsNullOrEmpty(author.Name))
            return BadRequest("Author name is required.");

        author.Id = BadDataStorage.Authors.Any() ? BadDataStorage.Authors.Max(a => a.Id) + 1 : 1;
        BadDataStorage.Authors.Add(author);

        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, Author updatedAuthor)
    {
        if (string.IsNullOrEmpty(updatedAuthor.Name))
            return BadRequest("Author name is required.");

        var author = BadDataStorage.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
            return NotFound($"Author with ID {id} was not found.");

        author.Name = updatedAuthor.Name;
        author.DateOfBirth = updatedAuthor.DateOfBirth;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        int index = BadDataStorage.Authors.FindIndex(a => a.Id == id);
        if (index == -1)
            return NotFound($"Author with ID {id} was not found.");

        BadDataStorage.Authors.RemoveAt(index);
        return NoContent();
    }
}