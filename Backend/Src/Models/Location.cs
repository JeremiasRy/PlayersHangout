namespace Backend.Src.Models;

using System.Text.Json.Serialization;

public class Location : BaseModel
{
    [JsonIgnore]
    public double Latitude { get; set; }
    [JsonIgnore]
    public double Longitude { get; set; }
    public City City { get; set; } = null!;
    public Guid CityId { get; set; }
    /// <summary>
    /// Calculate distance between two locations
    /// </summary>
    /// <param name="location">Another location</param>
    /// <returns>Distance between two points in KM</returns>
    public int DistanceFromAnotherLocation(Location location)
    {
        double radiusOfEarth = 6371e3; //in meters

        double latitudeOrigRadians = Latitude * Math.PI / 180;
        double latiduteToRadians = location.Latitude * Math.PI / 180;

        double latdiff = (location.Latitude - Latitude) * Math.PI / 180;
        double londiff = (location.Longitude - Longitude) * Math.PI / 180;

        double a = Math.Sin(latdiff / 2) * Math.Sin(latdiff / 2) + Math.Cos(latitudeOrigRadians) * Math.Cos(latiduteToRadians) * Math.Sin(londiff / 2) * Math.Sin(londiff / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distanceInMeters = radiusOfEarth * c;
        return (int)Math.Floor(distanceInMeters / 1000);
    }
}
