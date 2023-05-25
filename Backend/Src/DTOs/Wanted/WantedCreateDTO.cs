namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class WantedCreateDTO
{
    public Instrument Instrument { get; set; } = null!;
    public User.LevelOfCommitment Level { get; set; }
    public string Description { get; set; } = null!;
    public User User { get; set; } = null!;
    public ICollection<Genre> Genres { get; set; } = null!;
}