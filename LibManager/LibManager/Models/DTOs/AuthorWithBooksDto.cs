namespace LibManager.Models.DTOs;

public class AuthorWithBooksDto : AuthorDto
{
    public ICollection<BookDto> Books { get; set; }
}