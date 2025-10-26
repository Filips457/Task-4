namespace LibManager.Models.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}