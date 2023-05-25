namespace Backend.Src.DTOs;

using Backend.Src.Models;
using System.ComponentModel.DataAnnotations;

public class AuthSignUpDTO
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    public Guid? CityId { get; set; }
    public string? City { get; set; }
    public User.LevelOfCommitment Level { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection<GenreDTO>? Genres { get; set; }
    public ICollection<UserInstrumentDTO>? Instruments { get; set; }
}