using LibManager.Models.DTOs;
using LibManager.Models.Entities;
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
    public ActionResult<IEnumerable<BookDto>> GetAllBooks()
    {
        return bookServ.GetAllBooks();
    }

    [HttpGet("after_2015")]
    public ActionResult<IEnumerable<BookDto>> GetBooksAfter_2015()
    {
        return bookServ.GetBooksAfter_2015();
    }

    [HttpGet("{id}")]
    public ActionResult<BookDto> GetBookById([FromRoute] int id)
    {
        try
        {
            return bookServ.GetBookById(id);
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult InsertBook([FromBody] BookDto bookDto)
    {
        try
        {
            var insertedBook = bookServ.InsertBook(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = insertedBook.Id }, insertedBook);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook([FromRoute] int id, [FromBody] BookDto updatedBook)
    {
        try
        {
            bookServ.UpdateBook(id, updatedBook);
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
    public IActionResult DeleteBook([FromRoute] int id)
    {
        try
        {
            bookServ.DeleteBook(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
    }
}