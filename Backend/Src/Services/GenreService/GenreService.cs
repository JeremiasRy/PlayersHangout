using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public class GenreService : BaseServiceName<Genre, GenreDTO, GenreDTO, GenreDTO>, IGenreService
{
    public GenreService(IGenreRepo repo, IConverter converter) : base((GenreRepo)repo, converter)
    {
    }
}