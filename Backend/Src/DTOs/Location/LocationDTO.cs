namespace Backend.Src.DTOs;

public class LocationCreateDTO
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Guid CityId { get; set; }
}
