namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class UserInstrumentUpdateDTO
{
    public bool? LookingToPlay { get; set; }
    public UserInstrument.SkillLevel? SkillLevel;
}
