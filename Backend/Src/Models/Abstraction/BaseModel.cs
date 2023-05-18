using System.ComponentModel.DataAnnotations;

namespace Backend.Src.Models;

public abstract class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
public abstract class HasName : BaseModel
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public override string ToString() => $"{Name}";
}
