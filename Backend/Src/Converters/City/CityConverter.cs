namespace Backend.Src.Converters;

using Backend.Src.Models;

public class CityConverter : ICityConverter
{
    public City ConvertReadDTO(City model) => model;

    public void CreateModel(City create, out City model) => model = new City() { Name = create.Name };

    public void UpdateModel(City model, City update) => model.Name = update.Name;
}
