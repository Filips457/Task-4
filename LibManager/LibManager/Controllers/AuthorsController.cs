using LibManager.Models.DTOs;
using LibManager.Services.AuthorServ;
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
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
    {
        return Ok(await authorServ.GetAuthors());
    }

    [HttpGet("authors-with-books")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsWithBooks()
    {
        return Ok(await authorServ.GetAuthorsWithBooks());
    }

    [HttpGet("search/{id}")]
    public async Task<ActionResult<AuthorDto>> GetAuthorById([FromRoute] int id)
    {
        try
        {
            return await authorServ.GetAuthorById(id);
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("search-by/{name}")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> SearchAuthorByName([FromRoute] string name)
    {
        return Ok(await authorServ.GetAuthorsByName(name));
    }

    [HttpPost]
    public async Task<IActionResult> InsertAuthor([FromBody] AuthorDto authorDto)
    {
        try
        {
            var insertedAuth = await authorServ.InsertAuthor(authorDto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = insertedAuth.Id }, insertedAuth);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorDto updatedAuthor)
    {
        try
        {
            await authorServ.UpdateAuthor(id, updatedAuthor);
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
    public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
    {
        try
        {
            await authorServ.DeleteAuthor(id);
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