namespace Backend.Src.Models;

public class Instrument : BaseModel
{
    public string Name { get; set; } = null!;
    public ICollection<UserInstrument> Users { get; set; } = null!;
}