using Backend.Src.Models;

namespace Backend.src.Models;

public class Genres : BaseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}   