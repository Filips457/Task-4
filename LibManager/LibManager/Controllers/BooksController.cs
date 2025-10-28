using LibManager.Models.DTOs;
using LibManager.Services.BookServ;
using Microsoft.AspNetCore.Mvc;

namespace LibManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService bookServ;

    public BooksController(IBookService bookService)
    {
        bookServ = bookService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
    {
        return await bookServ.GetAllBooks();
    }

    [HttpGet("after_2015")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksAfter_2015()
    {
        return await bookServ.GetBooksAfter_2015();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBookById([FromRoute] int id)
    {
        try
        {
            return await bookServ.GetBookById(id);
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertBook([FromBody] BookDto bookDto)
    {
        try
        {
            var insertedBook = await bookServ.InsertBook(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = insertedBook.Id }, insertedBook);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDto updatedBook)
    {
        try
        {
            await bookServ.UpdateBook(id, updatedBook);
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
    public async Task<IActionResult> DeleteBook([FromRoute] int id)
    {
        try
        {
            await bookServ.DeleteBook(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
    }
}