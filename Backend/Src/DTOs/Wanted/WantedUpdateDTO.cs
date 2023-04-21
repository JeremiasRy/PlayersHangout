namespace Backend.Src.DTOs.Wanted;

public class WantedUpdateDTO
{
    public string Description { get; set; } = null!;
    public bool Fullfilled { get; set; }
}