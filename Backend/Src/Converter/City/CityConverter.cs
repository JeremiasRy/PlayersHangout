namespace Backend.Src.Converter.City;

using Backend.Src.Models;

public class CityConverter : ICityConverter
{
    public City ConvertReadDTO(City model) => model;

    public void CreateModel(City model, City create) => model.Name = create.Name;

    public void UpdateModel(City model, City update) => model.Name = update.Name;
}
