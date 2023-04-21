namespace Backend.Src.DTOs.Genre;

using System.ComponentModel.DataAnnotations;

public class GenreDTO
{
    [Required]
    public string Name { get; set; } = null!;
}