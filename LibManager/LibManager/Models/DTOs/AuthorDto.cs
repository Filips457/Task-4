using System.ComponentModel.DataAnnotations;

namespace LibManager.Models.DTOs;

public class AuthorDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
}