using LibManager.Models.DTOs;
using LibManager.Models.Entities;

namespace LibManager.Repository.BookRep;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooks();

    ValueTask<Book?> GetBookById(int id);

    Task<Book>InsertBook(Book bookToInsert);

    Task UpdateBook(Book bookToUpdate);

    Task DeleteBook(Book bookToDelete);
}