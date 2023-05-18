namespace Backend.Src.DTOs;

public class LocationDTO
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Guid? CityId { get; set; }
    // If city doen't exist yet in our db use name to create
    public string? City { get; set; }
}
