using LibManager.Data;
using LibManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryContext libContext;

    public BooksController(LibraryContext libraryContext)
    {
        libContext = libraryContext;
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        return libContext.Books.ToList();
    }

    [HttpGet("after_2015")]
    public ActionResult<IEnumerable<Book>> GetBooksAfter_2015()
    {
        return libContext.Books.Where(b => b.PublishedYear > 2015).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = libContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);

        if (book == null)
            return NotFound($"Book with ID {id} was not found.");

        return book;
    }

    [HttpPost]
    public IActionResult InsertBook(Book book)
    {
        if (string.IsNullOrEmpty(book.Title))
            return BadRequest("Book title is required.");

        if (libContext.Authors.Any(a => a.Id == book.AuthorId) == false)
            return BadRequest($"Author with ID {book.AuthorId} does not exist.");

        libContext.Books.Add(book);
        libContext.SaveChanges();

        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, Book updatedBook)
    {
        if (string.IsNullOrEmpty(updatedBook.Title))
            return BadRequest("Book title is required.");

        var book = libContext.Books.Find(id);
        if (book == null)
            return NotFound($"Book with ID {id} was not found.");

        book.Title = updatedBook.Title;
        book.PublishedYear = updatedBook.PublishedYear;
        book.AuthorId = updatedBook.AuthorId;
        libContext.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = libContext.Books.Find(id);
        if (book == null)
            return NotFound($"Book with ID {id} was not found.");

        libContext.Books.Remove(book);
        libContext.SaveChanges();

        return NoContent();
    }
}