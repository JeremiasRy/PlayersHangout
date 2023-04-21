namespace Backend.Src.Converter.Genre;

using Backend.Src.DTOs.Genre;
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

    public void CreateModel(Genre model, GenreDTO create)
    {
        model.Name = create.Name;
    }

    public void UpdateModel(Genre model, GenreDTO update)
    {
        model.Name = update.Name;
    }
}