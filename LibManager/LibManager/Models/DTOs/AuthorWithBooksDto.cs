namespace LibManager.Models.DTOs;

public class AuthorWithBooksDto : AuthorDto
{
    public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
}