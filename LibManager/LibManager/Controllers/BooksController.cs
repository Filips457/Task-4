using LibManager.Data;
using LibManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        return BadDataStorage.Books;
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = BadDataStorage.Books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return NotFound($"Book with ID {id} was not found.");

        return book;
    }

    [HttpPost]
    public IActionResult InsertBook(Book book)
    {
        if (string.IsNullOrEmpty(book.Title))
            return BadRequest("Book title is required.");

        book.Id = BadDataStorage.Books.Any() ? BadDataStorage.Books.Max(b => b.Id) + 1 : 1;
        BadDataStorage.Books.Add(book);

        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, Book updatedBook)
    {
        if (string.IsNullOrEmpty(updatedBook.Title))
            return BadRequest("Book title is required.");

        var book = BadDataStorage.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound($"Book with ID {id} was not found.");

        book.Title = updatedBook.Title;
        book.PublishedYear = updatedBook.PublishedYear;
        book.AuthorId = updatedBook.AuthorId;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        int index = BadDataStorage.Books.FindIndex(b => b.Id == id);
        if (index == -1)
            return NotFound($"Book with ID {id} was not found.");

        BadDataStorage.Books.RemoveAt(index);
        return NoContent();
    }
}