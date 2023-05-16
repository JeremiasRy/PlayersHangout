namespace Backend.Src.Models;

public class City : HasName
{
    public ICollection<Location> Locations { get; set; } = null!;
}
