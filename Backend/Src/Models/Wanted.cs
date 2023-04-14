namespace Backend.Src.Models;

public class Wanted : BaseModel
{
    public Instrument Instrument { get; set; } = null!;
    public Instrument.SkillLevel SkillLevel { get; set; }
    public string Description { get; set; } = null!;
    public User User { get; set; } = null!;
}
