namespace Backend.Src.DTOs;

using System.ComponentModel.DataAnnotations;

public class GenreDTO
{
    [Required]
    public string Name { get; set; } = null!;
}