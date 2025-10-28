using LibManager.Data;
using LibManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibManager.Repository.BookRep;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext libContext;

    public BookRepository(LibraryContext libraryContext)
    {
        libContext = libraryContext;
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return await libContext.Books.ToListAsync();
    }

    public async ValueTask<Book?> GetBookById(int id)
    {
        return await libContext.Books.FindAsync(id);
    }

    public async Task<Book> InsertBook(Book bookToInsert)
    {
        await libContext.Books.AddAsync(bookToInsert);
        await libContext.SaveChangesAsync();
        return bookToInsert;
    }

    public async Task UpdateBook(Book bookToUpdate)
    {
        libContext.Books.Update(bookToUpdate);
        await libContext.SaveChangesAsync();
    }

    public async Task DeleteBook(Book bookToDelete)
    {
        libContext.Books.Remove(bookToDelete);
        await libContext.SaveChangesAsync();
    }
}