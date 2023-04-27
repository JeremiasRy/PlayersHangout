namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class UserInstrumentReadDTO
{
    public string Instrument { get; set; } = null!;
    public UserInstrument.SkillLevel SkillLevel { get; set; }

}
