namespace Backend.Src.Models;

public class Wanted : BaseModel
{
    public Instrument Instrument { get; set; } = null!;
    public UserInstrument.SkillLevel SkillLevel { get; set; }
    public string Description { get; set; } = null!;
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public ICollection<Genre> Genres { get; set; } = null!;
    public bool Fullfilled { get; set; } = false;
}
