namespace Backend.Src.Converter.Genre;

using Backend.Src.DTOs.Genre;
using Backend.Src.Models;

public class GenreConverter : IGenreConverter
{
    public GenreReadDTO ConvertReadDTO(Genre model)
    {
        return new GenreReadDTO
        {
            Name = model.Name
        };
    }

    public void CreateModel(Genre model, GenreCreateDTO create)
    {
        model.Name = create.Name;
    }

    public void UpdateModel(Genre model, GenreUpdateDTO update)
    {
        model.Name = update.Name;
    }
}