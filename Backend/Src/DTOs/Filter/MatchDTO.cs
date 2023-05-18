namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class MatchDTO : BaseQueryOptions // works both ways wanted -> user || user -> wanted
{
    public string? City { get; set; } = null!;
    public UserInstrument.SkillLevel? SkillLevel { get; set; }
    public string? Instrument { get; set; }
    public string? Genre { get; set; }
}
