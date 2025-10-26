using System.ComponentModel.DataAnnotations.Schema;

namespace LibManager.Models.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PublishedYear { get; set; }

    [ForeignKey("Author")]
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}