using Backend.Src.Models;

namespace Backend.Src.DTOs.Wanted;

public class WantedReadDTO
{
    public string? City { get; set; }
    public string Description { get; set; } = null!;
    public string Instrument { get; set; } = null!;
    public UserInstrument.SkillLevel SkillLevel { get; set; }
}