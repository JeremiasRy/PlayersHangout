using System.Text.Json.Serialization;

namespace Backend.Src.Models;
public class Location : BaseModel
{
    [JsonIgnore]
    public double Latitude { get; set; }
    [JsonIgnore]
    public double Longitude { get; set; }
    public string? City { get; set; }
}
