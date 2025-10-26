using LibManager.Data;
using LibManager.Models.Entities;

namespace LibManager.Repository.BookRep;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext libContext;

    public BookRepository(LibraryContext libraryContext)
    {
        libContext = libraryContext;
    }

    public List<Book> GetAllBooks()
    {
        return libContext.Books.ToList();
    }

    public Book? GetBookById(int id)
    {
        return libContext.Books.Find(id);
    }

    public Book InsertBook(Book bookToInsert)
    {
        libContext.Books.Add(bookToInsert);
        libContext.SaveChanges();
        return bookToInsert;
    }

    public void UpdateBook(Book bookToUpdate)
    {
        libContext.Books.Update(bookToUpdate);
        libContext.SaveChanges();
    }

    public void DeleteBook(Book bookToDelete)
    {
        libContext.Books.Remove(bookToDelete);
        libContext.SaveChanges();
    }
}