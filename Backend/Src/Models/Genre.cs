using Backend.Src.Models;

namespace Backend.src.Models;

public class Genre : BaseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;    
}   