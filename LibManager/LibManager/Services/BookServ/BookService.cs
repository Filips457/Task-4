using LibManager.Models.DTOs;
using LibManager.Models.Entities;
using LibManager.Repository.BookRep;

namespace LibManager.Services.BookServ;

public class BookService : IBookService
{
    private readonly IBookRepository bookRep;

    public BookService(IBookRepository bookRepository)
    {
        bookRep = bookRepository;
    }

    public List<BookDto> GetAllBooks()
    {
        return bookRep.GetAllBooks().Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            PublishedYear = b.PublishedYear,
            AuthorId = b.AuthorId
        }).ToList();
    }

    public List<BookDto> GetBooksAfter_2015()
    {
        return bookRep.GetAllBooks().Where(b => b.PublishedYear > 2015).Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            PublishedYear = b.PublishedYear,
            AuthorId = b.AuthorId
        }).ToList();
    }

    public BookDto GetBookById(int id)
    {
        var book = bookRep.GetBookById(id);

        if (book == null)
            throw new Exception($"Book with ID {id} was not found.");

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };
    }

    public BookDto InsertBook(BookDto bookToInsert)
    {
        var book = new Book
        {
            Title = bookToInsert.Title,
            PublishedYear = bookToInsert.PublishedYear,
            AuthorId = bookToInsert.AuthorId
        };
        bookRep.InsertBook(book);

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };
    }

    public void UpdateBook(int id, BookDto bookToUpdate)
    {
        var book = bookRep.GetBookById(id);

        if (book == null)
            throw new Exception($"Book with ID {id} was not found.");

        bookRep.UpdateBook(book);
    }

    public void DeleteBook(int id)
    {
        var book = bookRep.GetBookById(id);

        if (book == null)
            throw new Exception($"Book with ID {id} was not found.");

        bookRep.DeleteBook(book);
    }
}