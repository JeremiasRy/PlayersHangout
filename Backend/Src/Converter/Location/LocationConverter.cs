namespace Backend.Src.Converter.Location;

using Backend.Src.DTOs.Location;
using Backend.Src.Models;

public class LocationConverter : ILocationConverter
{
    public LocationReadDTO ConvertReadDTO(Location model)
    {
        return new LocationReadDTO()
        {
            City = model.City.Name,
        };
    }

    public void CreateModel(Location model, LocationCreateDTO create)
    {
        model.CityId = create.CityId;
        model.Latitude = create.Latitude;
        model.Longitude = create.Longitude;
    }

    public void UpdateModel(Location model, LocationCreateDTO update)
    {
        model.CityId = update.CityId;
        model.Latitude = update.Latitude;
        model.Longitude = update.Longitude;
    }
}
