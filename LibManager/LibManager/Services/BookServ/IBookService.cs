using LibManager.Models.DTOs;

namespace LibManager.Services.BookServ;

public interface IBookService
{
    List<BookDto> GetAllBooks();

    List<BookDto> GetBooksAfter_2015();

    BookDto GetBookById(int id);

    BookDto InsertBook(BookDto bookToInsert);

    void UpdateBook(int id, BookDto bookToUpdate);

    void DeleteBook(int id);
}