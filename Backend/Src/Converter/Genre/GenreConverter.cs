namespace Backend.src.Converter.Genre;

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
}