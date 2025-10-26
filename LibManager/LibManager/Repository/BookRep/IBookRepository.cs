using LibManager.Models.DTOs;
using LibManager.Models.Entities;

namespace LibManager.Repository.BookRep;

public interface IBookRepository
{
    List<Book> GetAllBooks();

    Book? GetBookById(int id);

    Book InsertBook(Book bookToInsert);

    void UpdateBook(Book bookToUpdate);

    void DeleteBook(Book bookToDelete);
}