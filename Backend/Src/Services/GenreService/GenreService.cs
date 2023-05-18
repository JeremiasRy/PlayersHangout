using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public class GenreService : BaseServiceNameFilter<Genre, GenreDTO, GenreDTO, GenreDTO>, IGenreService
{
    public GenreService(IGenreRepo repo, IConverter converter) : base((GenreRepo)repo, converter)
    {
    }
}