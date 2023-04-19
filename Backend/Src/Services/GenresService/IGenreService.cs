namespace Backend.src.Services.GenresService;

using Backend.src.DTOs.Genre;
using Backend.src.Models;
using Backend.src.Services.BaseService;

public interface IGenreService : IBaseService<Genre, GenreReadDTO, GenreCreateDTO, GenreUpdateDTO>
{}