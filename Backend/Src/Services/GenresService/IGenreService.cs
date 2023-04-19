namespace Backend.Src.Services.GenresService;

using Backend.Src.DTOs.Genre;
using Backend.Src.Models;
using Backend.Src.Services.BaseService;

public interface IGenreService : IBaseService<Genre, GenreReadDTO, GenreCreateDTO, GenreUpdateDTO>
{}