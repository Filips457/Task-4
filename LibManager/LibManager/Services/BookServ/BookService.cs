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

    public async Task<List<BookDto>> GetAllBooks()
    {
        var books = await bookRep.GetAllBooks();

        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            PublishedYear = b.PublishedYear,
            AuthorId = b.AuthorId
        }).ToList();
    }

    public async Task<List<BookDto>> GetBooksAfter_2015()
    {
        var books = await bookRep.GetAllBooks();

        return books.Where(b => b.PublishedYear > 2015).Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            PublishedYear = b.PublishedYear,
            AuthorId = b.AuthorId
        }).ToList();
    }

    public async Task<BookDto> GetBookById(int id)
    {
        var book = await bookRep.GetBookById(id);

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

    public async Task<BookDto> InsertBook(BookDto bookDto)
    {
        var bookToInsert = new Book
        {
            Title = bookDto.Title,
            PublishedYear = bookDto.PublishedYear,
            AuthorId = bookDto.AuthorId
        };

        var bookToReturn = await bookRep.InsertBook(bookToInsert);

        return new BookDto
        {
            Id = bookToReturn.Id,
            Title = bookToReturn.Title,
            PublishedYear = bookToReturn.PublishedYear,
            AuthorId = bookToReturn.AuthorId
        };
    }

    public async Task UpdateBook(int id, BookDto bookDto)
    {
        var bookToUpdate = await bookRep.GetBookById(id);
        if (bookToUpdate == null)
            throw new Exception($"Book with ID {id} was not found.");

        bookToUpdate.Title = bookDto.Title;
        bookToUpdate.PublishedYear = bookDto.PublishedYear;
        bookToUpdate.AuthorId = bookDto.AuthorId;

        await bookRep.UpdateBook(bookToUpdate);
    }

    public async Task DeleteBook(int id)
    {
        var book = await bookRep.GetBookById(id);

        if (book == null)
            throw new Exception($"Book with ID {id} was not found.");

        await bookRep.DeleteBook(book);
    }
}