namespace Backend.Src.Converters;

using Backend.Src.DTOs;
using Backend.Src.Models;

public class CityConverter : ICityConverter
{
    public CityDTO ConvertReadDTO(City model) => new CityDTO() { Name = model.Name };

    public void CreateModel(CityDTO create, out City model)
    {
        model = new();
        model.Name = create.Name;
    }

    public void UpdateModel(City model, CityDTO update) => model.Name = update.Name;
}
