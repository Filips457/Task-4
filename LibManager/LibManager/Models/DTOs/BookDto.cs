using System.ComponentModel.DataAnnotations;

namespace LibManager.Models.DTOs;

public class BookDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}