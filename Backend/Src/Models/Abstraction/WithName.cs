using System.ComponentModel.DataAnnotations;

namespace Backend.Src.Models;

public abstract class WithName : BaseModel
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public override string ToString() => $"{Name}";
}
