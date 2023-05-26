namespace Backend.Src.DTOs;

using Backend.Src.Models;
using Backend.Src.Repositories;

public class MatchDTO : BaseQueryOptions // works both ways wanted -> user || user -> wanted
{
    public string? City { get; set; } = null!;
    public int MaxKm { get; set; }
    public User.LevelOfCommitment Level { get; set; }
    public string? Instrument { get; set; }
    public string? Genre { get; set; }
}
