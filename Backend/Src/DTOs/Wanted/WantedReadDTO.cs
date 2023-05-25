namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class WantedReadDTO
{
    public string? City { get; set; }
    public string Description { get; set; } = null!;
    public string Instrument { get; set; } = null!;
    public ICollection<Genre> Genres { get; set; } = null!;
    public User.LevelOfCommitment Level { get; set; }
}