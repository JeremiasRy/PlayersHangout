namespace Backend.Src.Models;

public class Instrument : BaseModel
{
    public string Name { get; set; } = null!;
    public SkillLevel Skill { get; set; } = SkillLevel.Beginner;

    public enum SkillLevel
    {
        // 0 < x < 5
        Beginner,
        // 5 < x < 15
        Intermediate,
        // x > 15
        Experienced
    }
}