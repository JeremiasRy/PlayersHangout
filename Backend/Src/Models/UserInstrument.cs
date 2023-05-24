using System.Text.Json.Serialization;

namespace Backend.Src.Models;

public class UserInstrument
{
    [JsonIgnore]
    public User User { get; set; } = null!;
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public Instrument Instrument { get; set; } = null!;
    [JsonIgnore]
    public Guid InstrumentId { get; set; }
    [JsonIgnore]
    public bool LookingToPlay { get; set; }
    public bool IsMain { get; set; }
    public SkillLevel Skill { get; set; }
    public override string ToString() => $"{Instrument.Name}";
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
