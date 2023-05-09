namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class UserInstrumentDTO
{
    public Guid UserId { get; set; }
    public Guid? InstrumentId { get; set; }
    public string? Instrument { get; set; }
    public bool LookingToPlay { get; set; }
    public UserInstrument.SkillLevel SkillLevel { get; set; }
}
