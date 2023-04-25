namespace Backend.Src.DTOs.Location;

public class LocationCreateDTO
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Guid CityId { get; set; }
}
