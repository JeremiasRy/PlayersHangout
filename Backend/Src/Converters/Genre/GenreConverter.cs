namespace Backend.Src.Converters;

using Backend.Src.DTOs;
using Backend.Src.Models;

public class GenreConverter : IGenreConverter
{
    public GenreDTO ConvertReadDTO(Genre model)
    {
        return new GenreDTO
        {
            Name = model.Name
        };
    }

    public void CreateModel(GenreDTO create, out Genre model) => model = new Genre() { Name = create.Name };

    public void UpdateModel(Genre model, GenreDTO update)
    {
        model.Name = update.Name;
    }
}