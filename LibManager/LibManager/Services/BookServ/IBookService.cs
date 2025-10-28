using LibManager.Models.DTOs;

namespace LibManager.Services.BookServ;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooks();

    Task<List<BookDto>> GetBooksAfter_2015();

    Task<BookDto> GetBookById(int id);

    Task<BookDto> InsertBook(BookDto bookDto);

    Task UpdateBook(int id, BookDto bookDto);

    Task DeleteBook(int id);
}