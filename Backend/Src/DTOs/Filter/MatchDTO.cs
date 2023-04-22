namespace Backend.Src.DTOs.Filter;

using Backend.Src.Models;
using Backend.Src.Repositories.BaseRepo;

public class MatchDTO : IFilterOptions // works both ways wanted -> user || user -> wanted
{
    public string? City { get; set; } = null!;
    public UserInstrument.SkillLevel SkillLevel { get; set; }
    public string? Instrument { get; set; }
    public string? Genre { get; set; }
}
