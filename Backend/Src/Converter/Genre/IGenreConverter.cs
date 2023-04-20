namespace Backend.Src.Converter.Genre;

using Backend.Src.DTOs.Genre;
using Backend.Src.Models;

public interface IGenreConverter : IConverter<Genre, GenreReadDTO, GenreCreateDTO, GenreUpdateDTO>
{}