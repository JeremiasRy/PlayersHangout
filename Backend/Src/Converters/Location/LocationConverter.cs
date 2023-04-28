namespace Backend.Src.Converters;

using Backend.Src.DTOs;
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

    public void CreateModel(LocationCreateDTO create, out Location model)
    {
        if (create.CityId == null)
        {
            throw new Exception("City Id was not initialized");
        }
        model = new Location()
        {
            CityId = (Guid)create.CityId,
            Latitude = create.Latitude,
            Longitude = create.Longitude
        };

    }

    public void UpdateModel(Location model, LocationCreateDTO update)
    {
        model.CityId = (Guid)update.CityId;
        model.Latitude = update.Latitude;
        model.Longitude = update.Longitude;
    }
}
