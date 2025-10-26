using LibManager.Models.DTOs;
using LibManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService authorServ;

    public AuthorsController(IAuthorService authorService)
    {
        authorServ = authorService;
    }

    [HttpGet("authors")]
    public ActionResult<IEnumerable<AuthorDto>> GetAllAuthors()
    {
        return Ok(authorServ.GetAuthors());
    }

    [HttpGet("authors-with-books")]
    public ActionResult<IEnumerable<AuthorDto>> GetAuthorsWithBooks()
    {
        return Ok(authorServ.GetAuthorsWithBooks());
    }

    [HttpGet("search/{id}")]
    public ActionResult<AuthorDto> GetAuthorById([FromRoute] int id)
    {
        try
        {
            return authorServ.GetAuthorById(id);
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("search-by/{name}")]
    public ActionResult<IEnumerable<AuthorDto>> SearchAuthorByName([FromRoute] string name)
    {
        return Ok(authorServ.GetAuthorsByName(name));
    }

    [HttpPost]
    public IActionResult InsertAuthor([FromBody] AuthorDto authorDto)
    {
        try
        {
            var insertedAuth = authorServ.InsertAuthor(authorDto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = insertedAuth.Id }, insertedAuth);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor([FromRoute] int id, [FromBody] AuthorDto updatedAuthor)
    {
        try
        {
            authorServ.UpdateAuthor(id, updatedAuthor);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor([FromRoute] int id)
    {
        try
        {
            authorServ.DeleteAuthor(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}