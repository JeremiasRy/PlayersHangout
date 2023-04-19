namespace Backend.src.Converter.Genre;

using Backend.src.DTOs.Genre;
using Backend.src.Models;

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