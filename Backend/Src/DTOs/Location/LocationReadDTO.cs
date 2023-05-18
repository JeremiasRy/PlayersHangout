namespace Backend.Src.DTOs;

public class LocationReadDTO
{
    public string City { get; set; } = null!;
    public int DistanceFromYouInKm { get; set; }
}
