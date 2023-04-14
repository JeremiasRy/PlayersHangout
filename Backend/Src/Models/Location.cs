namespace Backend.Src.Models;

public class Location : BaseModel
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? City { get; set; }
}
